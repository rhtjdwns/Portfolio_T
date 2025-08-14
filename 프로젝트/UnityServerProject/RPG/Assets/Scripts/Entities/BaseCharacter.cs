using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.IO.LowLevel.Unsafe;

public class BaseCharacter : NetworkBehaviour
{
    [Header("Base Unit Setting")]
    [SerializeField] protected BaseStat stat;
    public BaseStat Stat => individualStat != null ? individualStat : stat;
    [SerializeField] protected Animator anim;
    public Animator Anim => anim;
    [SerializeField] protected GameObject unitModel;
    public GameObject UnitModel => unitModel;
    private BaseStat individualStat;

    [Header("Unit Network Stat")]
    [SerializeField] private NetworkVariable<float> hp = new NetworkVariable<float>(
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );
    public float Hp { get => hp.Value; set => hp.Value = value; }
    
    private float localAttackDamage;

    public bool Alive => Hp > 0;

    public readonly StateMachine stateMachine = new();

    public Rigidbody Rb { get; private set; }

    protected virtual void Awake() 
    {
        Rb = GetComponent<Rigidbody>();
        
        // 원본 stat을 복사하여 개별적인 인스턴스 생성
        if (stat != null)
        {
            individualStat = Instantiate(stat);
        }
    }

    protected virtual void Update()
    {
        stateMachine.StayCurrentState();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.FixedStayCurrentState();
    }

    [ServerRpc(RequireOwnership = false)]
    public virtual void ReceiveDamageServerRpc(float amount)
    {
        if (Alive == false)
        {
            return;
        }

        Hp = Mathf.Clamp(Hp - amount, 0, float.MaxValue);

        if (Hp <= 0)
        {
            Die();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateMaxHpServerRpc(float amount)
    {
        stat.MaxHp += amount;
        Hp = stat.MaxHp;
    }

    public virtual void Die()
    {
        Hp = 0;
    }

    [ServerRpc(RequireOwnership = false)]
    public void InitializeStatsServerRpc(float maxHp, float attackDamage, int bossGrade)
    {
        if (!IsServer) return;
        
        // individualStat이 없으면 생성
        if (individualStat == null && stat != null)
        {
            individualStat = Instantiate(stat);
        }
        
        individualStat.MaxHp = maxHp;
        individualStat.AttackDamage = attackDamage;
        individualStat.BossGrade = bossGrade;
        
        // 로컬 변수 설정 (네트워크 동기화되지 않음)
        Hp = individualStat.MaxHp;  // HP는 네트워크 변수로 동기화됨
        localAttackDamage = individualStat.AttackDamage;
        
        Debug.Log($"[BaseCharacter] 스탯 초기화 완료: HP={Hp}, 공격력={localAttackDamage}, BossGrade={individualStat.BossGrade}");
    }
}
