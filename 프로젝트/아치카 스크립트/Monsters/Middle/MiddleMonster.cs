using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MiddleMonster : Monster
{
    [Header("����")]
    [SerializeField] private Define.MiddleMonsterName _monsterName = Define.MiddleMonsterName.NONE;

    [Header("����")]
    [SerializeField] private Define.MiddleMonsterState _currentState = Define.MiddleMonsterState.NONE;                                   // ���� ����
    private Dictionary<Define.MiddleMonsterState, Middle_State> _stateStroage = new Dictionary<Define.MiddleMonsterState, Middle_State>(); // ���� �����

    [Header("��ų")]
    [SerializeField] private List<Middle_Skill> _skillStorage = new List<Middle_Skill>();  // ��ų �����
    [SerializeField] private Middle_Skill _currentSkill = null;                            // ���� ��ų
    [SerializeField] private List<Middle_Skill> _readySkills = new List<Middle_Skill>();   // �غ�� ��ų

    [Header("����")]
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private Vector3 _colliderSize;

    [Header("�и�")]
    [SerializeField] private Transform _parringPoint;
    [SerializeField] private Vector3 _parringColliderSize;

    [Header("Idle ���ð�")]
    [SerializeField] private float _idleDuration;                                        // ��� ���� �ð�

    [Header("���Ͽ� ����Ʈ")]
    public Dictionary<Define.MiddleMonsterPoint, Transform> middlePoint;

    [Header("�߰� �ƾ�")]
    [SerializeField] public GameObject curScene;

    [Header("�ǰ�")]
    private bool isHit;

    private MiddlePhaseManager manager;

    public Define.MiddleMonsterState CurrentState { get => _currentState; }
    public Define.MiddleMonsterName monsterName { get => _monsterName; }
    public Middle_Skill CurrentSkill { get => _currentSkill; }
    public List<Middle_Skill> ReadySkills { get => _readySkills; set => _readySkills = value; }
    public Transform HitPoint { get => _hitPoint; set => _hitPoint = value; }
    public List<Middle_Skill> SkillStorage { get => _skillStorage; }
    public Vector3 ColliderSize { get => _colliderSize; set => _colliderSize = value; }
    public float IdleDuration { get => _idleDuration; }
    public bool IsHit { get => isHit; set => isHit = value; }
    public int phase = 1;

    public Action OnAttackAction;
    public Action OnFinishSkill;

    protected override void Init()
    {
        manager = FindObjectOfType<MiddlePhaseManager>();
        _player = FindObjectOfType<Player>().transform;
        
        if (_monsterName == Define.MiddleMonsterName.CHEONG)
        {
            _stateStroage.Add(Define.MiddleMonsterState.IDLE, new Cheong_Idle(this));
        }
        else if (_monsterName == Define.MiddleMonsterName.GYEONGCHAE)
        {
            _stateStroage.Add(Define.MiddleMonsterState.IDLE, new Gyeongchae_Idle(this));
        }

        _stateStroage.Add(Define.MiddleMonsterState.USESKILL, new Middle_UseSkill(this));
        _stateStroage.Add(Define.MiddleMonsterState.GROGGY, new Middle_Groggy(this));
        _stateStroage.Add(Define.MiddleMonsterState.DIE, new Middle_Die(this));

        _stat.Init();
    }

    public void Enter()
    {
        foreach (Middle_Skill s in _skillStorage)
        {
            s.Init(this);
        }

        _currentState = Define.MiddleMonsterState.NONE;

        ChangeCurrentState(Define.MiddleMonsterState.IDLE);
    }

    public void Stay()
    {
        if (_currentState != Define.MiddleMonsterState.NONE)
        {
            _stateStroage[_currentState]?.Stay();
        }
    }

    public void ChangeCurrentState(Define.MiddleMonsterState state)
    {
        if (_currentState != Define.MiddleMonsterState.NONE)
        {
            _stateStroage[_currentState]?.Exit();
        }
        _currentState = state;

        if (_currentState != Define.MiddleMonsterState.NONE)
        {
            _stateStroage[_currentState]?.Enter();
        }
    }

    #region ��ų

    public void ReadySkill(Define.MiddleMonsterSkill skill)
    {
        GetSkill(skill).IsCompleted = true;
    }

    public void ChangeCurrentSkill(Define.MiddleMonsterSkill skill)
    {
        if (_currentSkill != null)
        {
            _skillStorage.Add(_currentSkill); // ���� ����ҷ� �̵�     
            _currentSkill?.Exit();
        }

        if (skill == Define.MiddleMonsterSkill.NONE)
        {
            _currentSkill = null;
        }
        else
        {
            _currentSkill = GetSkill(skill);

            if (_currentSkill == null)
            {
                Debug.Log("���� ��ų�� �������...");
                _currentSkill = GetReadySkill(skill);
            }

            _skillStorage.Remove(_currentSkill);
            _currentSkill.IsCompleted = true;
            _currentSkill?.Enter();
        }
    }
    public void ChangeCurrentSkill(Middle_Skill skill)
    {
        if (_currentSkill != null)
        {
            _skillStorage.Add(_currentSkill); // ���� ����ҷ� �̵�     
            _currentSkill?.Exit();
        }
        _currentSkill = skill;
        _currentSkill?.Enter();
    }

    public Middle_Skill GetSkill(Define.MiddleMonsterSkill skill)
    {
        foreach (Middle_Skill s in _skillStorage)
        {
            if (s.Info.skill == skill)
            {
                return s;
            }
        }

        return null;
    }

    public Middle_Skill GetReadySkill(Define.MiddleMonsterSkill skill)
    {
        foreach (Middle_Skill s in _readySkills)
        {
            if (s.Info.skill == skill)
            {
                return s;
            }
        }

        return null;
    }

    public void FinishSkill(Define.MiddleMonsterState state = Define.MiddleMonsterState.IDLE)
    {
        ChangeCurrentState(state);

        OnFinishSkill = null;
        OnAttackAction = null;
    }

    #endregion ��ų

    public void StartMiddleCut()
    {
        StartCoroutine(ExitCutScene());
    }

    private IEnumerator ExitCutScene()
    {
        curScene.SetActive(true);

        yield return new WaitForSeconds(8f);

        curScene.SetActive(false);

        yield return null;
    }

    public override void TakeDamage(float value)
    {
        manager.SetHp(value);
        if (monsterName == Define.MiddleMonsterName.GYEONGCHAE)
        {
            if (!isHit)
            {
                isHit = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.TransformPoint(new Vector3(_hitPoint.localPosition.x * -Direction, _hitPoint.localPosition.y, _hitPoint.localPosition.z)), _colliderSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_parringPoint.position, _parringColliderSize);
    }
}
