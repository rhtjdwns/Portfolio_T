using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class NormalMonster : Monster
{
    [System.Serializable]
    public class PerceptionRange
    {
        [Range(0, 100)]
        public int range_Ratio;
        public float gaugeIncrementPerSecond;
        public Color color;
    }

    #region ����
    private INomalMonsterView _nomalMonsterView;

    [SerializeField] private float _moveRange; // Idle �̵� �Ÿ�


    [Space]
    [SerializeField] private float _perceptionDistance;                                                                                    // �ν� �Ÿ�
    //[SerializeField] private float _perceptionAngle;                                                                                       // �ν� ����
    //[SerializeField] private List<PerceptionRange> _perceptionRanges = new List<PerceptionRange>();                                        // �ν� ����
    private Dictionary<Define.PerceptionType, Normal_State> _perceptionStateStorage = new Dictionary<Define.PerceptionType, Normal_State>(); // �ν� ���� �����
    [SerializeField] private Define.PerceptionType _currentPerceptionState;                                                                  // ���� �λ� ���� 
    private MonsterSkillSlot[] _currentSlots;
    private Transform _target;

    /*[SerializeField] private float _maxAggroGauge; // �ִ� ��׷� ������
    private float _aggroGauge = 0;                 // ���� ��׷� ������*/

    public Vector2 SpawnPoint { get; set; }


    [Space]
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private Vector3 _colliderSize;

    [Space]
    [Header("�ǰ� ���ð�")]
    [SerializeField] private float hittingTime = 0.3f;
    [Header("�ǰ� ���̴�")]
    [SerializeField] private Material hitShader;
    [SerializeField] private Material[] originShader;
    [SerializeField] private SkinnedMeshRenderer[] skinnedMesh;

    private float _hitTimer = 0f;
    private Coroutine _hitCoroutine;
    public bool isHit = false;
    public bool isHiting = false;
    public bool hitRock = false;

    [Space]
    [Header("������")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckRadius = 0.2f;

    [Space]
    [Header("���� ��ȯ ����")]
    public StateChangeConditions stateConditions;

    [Space]
    [Header("���� Ÿ��")]
    public Define.NormalMonsterType monsterType;

    [Space]
    [Header("�ñر� ����Ʈ")]
    [SerializeField] public GameObject ultimateEffect;
    [Header("�ñر� ä������ ��")]
    [SerializeField] public float ultimateValue;

    private bool isTrace = false;

    #endregion

    #region ������Ƽ
    public bool isAttack { get; set; }

    public Transform Target { get => _target; }
    public float MoveRange { get => _moveRange; }
    public Transform HitPoint { get => _hitPoint; set => _hitPoint = value; }
    public Vector3 ColliderSize { get => _colliderSize; set => _colliderSize = value; }
    public Transform GroundCheckPoint { get => _groundCheckPoint; }
    public float GroundCheckRadius { get => _groundCheckRadius; }
    public bool IsTrace { set => isTrace = value; get => isTrace; }



    public float PerceptionDistance { get => _perceptionDistance; }
    //public float PerceptionAngle { get => _perceptionAngle; }
    //public List<PerceptionRange> PerceptionRanges { get => _perceptionRanges; }
    public Define.PerceptionType PreviousPerceptionState { get; private set; }
    public Define.PerceptionType CurrentPerceptionState
    {
        get
        {
            return _currentPerceptionState;
        }
        set
        {
            // ���� �Ұ��� ���¶�� ���¸� ��ȯ���� ����
            // ��, �ƿ� Conditions�� �Ҵ���� ���� ��쿡�� �ش� ����� ������� �ʴ� ������ ���� -> ���� ����
            if (stateConditions != null)
            {
                if (!stateConditions.GetChangable((int)_currentPerceptionState, (int)value)) { return; }
            }

            ForceChangeState(value);
        }
    }

    /*public float MaxAggroGauge { get => _maxAggroGauge; }
    public float AggroGauge
    {
        get => _aggroGauge;
        set
        {

            if (value < 0)
            {
                _aggroGauge = 0;
            }
            else
            {
                _aggroGauge = value;

            }

            if (_aggroGauge > _maxAggroGauge)
            {
                _aggroGauge = _maxAggroGauge;
            }

        }
    }*/
    public MonsterSkillSlot[] CurrentSkillSlots { get => _currentSlots; set => _currentSlots = value; }
   

    #endregion

    protected override void Init()
    {
        _nomalMonsterView = GetComponent<INomalMonsterView>();

        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<Player>().transform;

        _stat.Init();
    }

    private void Start()
    {
        //_perceptionStateStorage.Add(Define.PerceptionType.PATROL, new Nomal_Patrol(this));
        //_perceptionStateStorage.Add(Define.PerceptionType.BOUNDARY, new Nomal_Boundary(this));
        _perceptionStateStorage.Add(Define.PerceptionType.IDLE, new Normal_IdleState(this));
        _perceptionStateStorage.Add(Define.PerceptionType.HIT, new Normal_HitState(this));
        _perceptionStateStorage.Add(Define.PerceptionType.TRACE, new Normal_TraceState(this));
        _perceptionStateStorage.Add(Define.PerceptionType.GUARD, new Normal_GuardState(this));
        _perceptionStateStorage.Add(Define.PerceptionType.DETECTIONM, new Normal_Detectionm(this));
        _perceptionStateStorage.Add(Define.PerceptionType.SKILLATTACK, new Normal_SkillAttackState(this));
        _perceptionStateStorage.Add(Define.PerceptionType.NORMALATTACK, new Normal_NormalAttackState(this));
        _perceptionStateStorage.Add(Define.PerceptionType.DEATH, new Normal_Death(this));

        CurrentPerceptionState = Define.PerceptionType.IDLE;

        _stat.Hp = _stat.MaxHp;

        _target = CharacterManager.Instance.GetCharacter(PlayerLayer.value)[0].transform;

        // �˹� �� �����ϴ� �̺�Ʈ
        OnKnockback += () =>
        {
            float dir = _player.position.x - transform.position.x;
            Direction = dir;
        };

        isAttack = true;
    }

    protected override void Update()
    {
        //CheckPerceptionState(); // ������ ���� ��Ű�� �Լ�
        //UpdatePerceptionState(); // ������ Ȯ�� �� �ν� ���� ������Ʈ

        if (_skillManager != null)
        {
            _skillManager.OnUpdate(this);
        }

        // �ν� ���� �ȿ� ������ ��
        /*if (_perceptionStateStorage[_currentPerceptionState].IsEntered)
        {
            _perceptionStateStorage[_currentPerceptionState]?.Stay();
        }*/
        _perceptionStateStorage[_currentPerceptionState]?.Stay();
    }



    #region AggroLegacy
    /*// ��ä�� �ȿ� �÷��̾ �ִ��� Ȯ��
    private void CheckPerceptionState() 
    {
        #region legacy
        // ���������� �÷��̾�� ���ϴ� ���� ���

        Vector3 playerPos = new Vector3(_player.position.x, _player.position.y, _player.position.z);
        Vector3 directionToPlayer = playerPos - transform.position;
        directionToPlayer.z = 0; // ����(z��)�� ������� ����

        // �÷��̾���� �Ÿ� ���
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(new Vector3(_direction, 0, 0), directionToPlayer);

        if (CurrentPerceptionState == Define.PerceptionType.DETECTIONM)
        {
            if (distanceToPlayer > _perceptionDistance)
            {
                AggroGauge -= 20 * Time.deltaTime;
            }
        }
        else
        {
            if (distanceToPlayer > _perceptionDistance || angleToPlayer > _perceptionAngle / 2.0f) // �÷��̾ ��ä���� ������ ���� ���� ��
            {
                AggroGauge -= 20 * Time.deltaTime;
            }
            else // �÷��̾ ��ä���� ������ ���� ���� ��
            {
                // ������ ����
                if (distanceToPlayer <= _perceptionDistance * ((float)_perceptionRanges[0].range_Ratio / 100.0f))
                {
                    AggroGauge += _perceptionRanges[0].gaugeIncrementPerSecond * Time.deltaTime;
                }
                else if (distanceToPlayer <= _perceptionDistance * ((float)_perceptionRanges[1].range_Ratio / 100.0f))
                {
                    AggroGauge += _perceptionRanges[1].gaugeIncrementPerSecond * Time.deltaTime;
                }
                else if (distanceToPlayer <= _perceptionDistance * ((float)_perceptionRanges[2].range_Ratio / 100.0f))
                {
                    AggroGauge += _perceptionRanges[2].gaugeIncrementPerSecond * Time.deltaTime;
                }

            }
        }

        UpdatePerceptionGauge();
    }

    // �ν� ���� ��ȭ ������Ʈ �Լ�
    private void UpdatePerceptionState()
    {
        if (_aggroGauge == 0)
        {
            if (CurrentPerceptionState != Define.PerceptionType.IDLE)
            {
                CurrentPerceptionState = Define.PerceptionType.IDLE;
            }
        }
        else if (_aggroGauge == 10)
        {
            if (CurrentPerceptionState != Define.PerceptionType.DETECTIONM)
            {
                CurrentPerceptionState = Define.PerceptionType.DETECTIONM;
            }
        }
        else
        {
            if (CurrentPerceptionState != Define.PerceptionType.BOUNDARY)
            {
                CurrentPerceptionState = Define.PerceptionType.BOUNDARY;
            }
        }
    }*/
    #endregion

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);

        if (skinnedMesh.Length > 0)
        {
            if (_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
            }

            if (monsterType == Define.NormalMonsterType.MON3)
            {
                for (int i = 0; i < skinnedMesh.Length; ++i)
                {
                    originShader[i] = skinnedMesh[i].material;
                    skinnedMesh[i].material = hitShader;
                }
            }
            else
            {
                for (int i = 0; i < skinnedMesh.Length; ++i)
                {
                    skinnedMesh[i].material = hitShader;
                }
            }

            _hitCoroutine = StartCoroutine(TurnOffShader());
        }

        if (Stat.Hp <= 0)
        {
            CurrentPerceptionState = Define.PerceptionType.DEATH;
        }
        else
        {
            if (hitRock)
            {
                return;
            }

            bool isAnim = Ani.GetCurrentAnimatorStateInfo(0).IsTag("Hit");

            if (isHit && isAnim)
            {
                Rb.velocity = Vector3.zero;
                Rb.AddForce(new Vector3(0, 2.5f), ForceMode.VelocityChange);
                isHiting = true;
                return;
            }

            if (monsterType == Define.NormalMonsterType.BALDO)
            {
                int num = Random.Range(0, 2);
                if (num == 0)
                {
                    TestSound.Instance.PlaySound("NormalMonster1_Beat1");
                }
                else if (num == 1)
                {
                    TestSound.Instance.PlaySound("NormalMonster1_Beat2");
                }
            }
            else if (monsterType == Define.NormalMonsterType.KUNG)
            {
                int num = Random.Range(0, 2);
                if (num == 0)
                {
                    TestSound.Instance.PlaySound("NormalMonster2_Beat1");
                }
                else if (num == 1)
                {
                    TestSound.Instance.PlaySound("NormalMonster2_Beat2");
                }
            }
            else if (monsterType == Define.NormalMonsterType.MON3)
            {
                int num = Random.Range(0, 2);
                if (num == 0)
                {
                    TestSound.Instance.PlaySound("NormalMonster3_Beat1");
                }
                else if (num == 1)
                {
                    TestSound.Instance.PlaySound("NormalMonster3_Beat2");
                }
            }

            if (CurrentPerceptionState != Define.PerceptionType.HIT)
            {
                isHit = true;
                CurrentPerceptionState = Define.PerceptionType.HIT;
            }
        }
        return;
    }

    private IEnumerator TurnOffShader()
    {
        yield return new WaitForSeconds(0.02f);

        if (monsterType == Define.NormalMonsterType.MON3)
        {
            for (int i = 0; i < skinnedMesh.Length; ++i)
            {
                skinnedMesh[i].material = originShader[i];
            }
        }
        else
        {
            foreach (var skin in skinnedMesh)
            {
                skin.material = originShader[0];
            }
        }

        yield return null;
    }

    // ��ų ���� �ߵ� �������� ��ȯ
    public bool GetSkillAttackUsable()
    {
        if (_SkillManager == null)
        {
            return false;
        }

        var slots = _SkillManager.GetUsableSkillSlots();
        float distance = Vector3.Distance(transform.position, Player.position);

        if (slots.Length > 0)
        {
            CurrentSkillSlots = slots;
            if (CurrentSkillSlots[0].skillRunner.skillData.SkillEffectValue * SkillData.cm2m >= distance
                && CurrentSkillSlots[0].IsUsable(_SkillManager))
            {
                return true;
            }
        }

        return false;
    }

    // �Ϲ� ���� �ߵ� �������� ��ȯ
    public bool GetNormalAttackUsable()
    {
        float distance = Vector3.Distance(transform.position, Player.position);

        if (MonsterSt.AttackRange >= distance && isAttack)
        {
            return true;
        }

        return false;
    }

    // ��ų �Ǵ� �Ϲ� ���ݿ� �����ߴ��� ��ȯ
    public bool TryAttack()
    {
        if(GetSkillAttackUsable())
        {
            ForceChangeState(Define.PerceptionType.SKILLATTACK);
            return true;
        }
        else if(GetNormalAttackUsable())
        {
            CurrentPerceptionState = Define.PerceptionType.NORMALATTACK;
            if (monsterType == Define.NormalMonsterType.MON3)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    TestSound.Instance.PlaySound("NormalMonster3_HitVoice1");
                    TestSound.Instance.PlaySound("NormalMonster3_Attack1");
                    Ani.SetInteger("AttackCount", 0);
                }
                else
                {
                    TestSound.Instance.PlaySound("NormalMonster3_HitVoice2");
                    TestSound.Instance.PlaySound("NormalMonster3_Attack2");
                    Ani.SetInteger("AttackCount", 1);
                }
            }
            return true;
        }

        return false;
    }

    public void ForceChangeState(Define.PerceptionType nextState)
    {
        // �Էµ� ���� ����ҿ� ������ ���¸� ��ȯ���� ����
        if (!_perceptionStateStorage.ContainsKey(nextState)) { return; }

        if (_perceptionStateStorage.ContainsKey(_currentPerceptionState))
        {
            _perceptionStateStorage[_currentPerceptionState]?.Exit();
        }
        PreviousPerceptionState = _currentPerceptionState;
        _currentPerceptionState = nextState;
        _perceptionStateStorage[_currentPerceptionState]?.Enter();
    }

    /*    #region View

        public void UpdatePerceptionGauge()
        {
            _nomalMonsterView.UpdatePerceptionGaugeImage(_aggroGauge/_maxAggroGauge);
        }

        #endregion*/

    private void OnEnable()
    {
        MonsterSt.Hp = MonsterSt.MaxHp;
        GetComponent<NomalMonsterView>().SetFullHp();
        Rb.useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void StartHitTimer()
    {
        StartCoroutine(CheckHitTimer());
    }

    private IEnumerator CheckHitTimer()
    {
        while (isHit)
        {
            _hitTimer += Time.deltaTime;
            if (_hitTimer > hittingTime)
            {
                _hitTimer = 0;
                isHit = false;
            }

            yield return null;
        }
    }

    public override bool IsLeftDirection()
    {
        if (monsterType == Define.NormalMonsterType.KUNG || monsterType == Define.NormalMonsterType.MON3)
        {
            return Direction != 1;
        }

        return Direction != -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_hitPoint.position, _colliderSize);

        if (SpawnPoint != Vector2.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(new Vector3(SpawnPoint.x - _moveRange, transform.position.y, 0), new Vector3(SpawnPoint.x + _moveRange, transform.position.y, 0));
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_groundCheckPoint)
        {
            Gizmos.DrawWireSphere(_groundCheckPoint.position, _groundCheckRadius);
        }
    }
}
