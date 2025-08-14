using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Enemy : BaseCharacter
{
    private List<PlayerController> players = new List<PlayerController>();
    private NetworkVariable<ulong> currentTargetId = new NetworkVariable<ulong>(
        ulong.MaxValue,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    [Header("Animation")]
    private NetworkVariable<bool> isMoving = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );
    private NetworkVariable<bool> isAttacking = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    [Header("State")]
    [SerializeField] private Define.EnemyState currentState = Define.EnemyState.NONE;
    private Dictionary<Define.EnemyState, IState> stateDict = new Dictionary<Define.EnemyState, IState>();

    public PlayerController CurrentTarget => GetPlayerById(currentTargetId.Value);

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        stateDict.Add(Define.EnemyState.IDLE, new EnemyIdleState(this));
        stateDict.Add(Define.EnemyState.MOVE, new EnemyMoveState(this));
        stateDict.Add(Define.EnemyState.ATTACK, new EnemyAttackState(this));

        // 초기 상태는 IDLE로 설정
        stateMachine.ChangeState(new EnemyIdleState(this));
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        
        // 애니메이션 파라미터 초기화
        isMoving.OnValueChanged += (oldValue, newValue) => {
            if (anim != null)
            {
                anim.SetBool("IsMove", newValue);
            }
        };
        isAttacking.OnValueChanged += (oldValue, newValue) => {
            if (anim != null)
            {
                anim.SetBool("IsAttack", newValue);
            }
        };
        
        if (IsServer)
        {
            StartCoroutine(UpdateTargetRoutine());
            
            Hp = Stat.MaxHp;
        }
    }

    [ServerRpc]
    private void SetAnimationServerRpc(bool isMove, bool isAttack)
    {
        isMoving.Value = isMove;
        isAttacking.Value = isAttack;
    }

    public void SetAnimation(bool isMove, bool isAttack)
    {
        if (IsServer)
        {
            isMoving.Value = isMove;
            isAttacking.Value = isAttack;
        }
        else
        {
            SetAnimationServerRpc(isMove, isAttack);
        }
    }

    // 플레이어 등록
    public void RegisterPlayer(PlayerController player)
    {
        if (player == null)
        {
            return;
        }

        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    // 플레이어 제거
    public void UnregisterPlayer(PlayerController player)
    {
        if (player == null)
        {
            return;
        }

        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }

    // 가장 가까운 플레이어를 타겟으로 설정
    private IEnumerator UpdateTargetRoutine()
    {
        while (IsServer && IsSpawned)
        {
            UpdateNearestTarget();
            yield return new WaitForSeconds(0.5f); // 0.5초마다 타겟 갱신
        }
    }

    private void UpdateNearestTarget()
    {
        if (!IsServer || players.Count == 0)
            return;

        PlayerController nearest = null;
        float minDistance = float.MaxValue;

        foreach (var player in players)
        {
            if (player == null || !player.IsSpawned)
                continue;

            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = player;
            }
        }
        if (nearest != null)
            currentTargetId.Value = nearest.OwnerClientId;
        else
            currentTargetId.Value = ulong.MaxValue;
    }

    private PlayerController GetPlayerById(ulong clientId)
    {
        if (clientId == ulong.MaxValue)
        {
            return null;
        }
        var player = players.Find(p => p.OwnerClientId == clientId);

        return player;
    }

    // 헬퍼 메서드
    public bool HasValidTarget()
    {
        var hasTarget = CurrentTarget != null && CurrentTarget.IsSpawned;
        return hasTarget;
    }

    public Vector3 GetTargetPosition()
    {
        return HasValidTarget() ? CurrentTarget.transform.position : transform.position;
    }

    public float GetDistanceToTarget()
    {
        return HasValidTarget() ? 
            Vector3.Distance(transform.position, CurrentTarget.transform.position) : 
            float.MaxValue;
    }
}

