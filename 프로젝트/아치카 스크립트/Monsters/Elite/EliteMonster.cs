using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteMonster : Monster
{
    #region ����

    // ����
    private Dictionary<Define.EliteMonsterState, Elite_State> _stateStroage = new Dictionary<Define.EliteMonsterState, Elite_State>(); // ���� �����
    [SerializeField] private Define.EliteMonsterState _currentState = Define.EliteMonsterState.NONE;                                   // ���� ����

    [Header("�׷α�")]
    [SerializeField] private float _groggyTime;
    [Header("����")]
    [SerializeField] private float _failTime;
    [Header("����")]
    [SerializeField] private float _dieTime;

    // ��ų
    [SerializeField] private List<Elite_Skill> _skillStorage = new List<Elite_Skill>();  // ��ų �����
    [SerializeField] private Elite_Skill _currentSkill = null;                           // ���� ��ų
    [SerializeField] private List<Elite_Skill> _readySkills = new List<Elite_Skill>();   // �غ�� ��ų

    [SerializeField] private float _idleDuration;                                        // ��� ���� �ð�

    [Header("����")]
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private Vector3 _colliderSize;
    [Header("�и�")]
    [SerializeField] private Transform _parringPoint;
    [SerializeField] private Vector3 _parringColliderSize;
    [Header("����")]
    [SerializeField] private CreatePlatform _createPlatform;

    public Action OnAttackAction;
    public Action OnFinishSkill;
    #endregion;

    #region ������Ƽ

    public Define.EliteMonsterState CurrentState { get => _currentState; }
    public float GroggyTime { get => _groggyTime; set => _groggyTime = value; }
    public float FailTime { get => _failTime; set => _failTime = value; }
    public float DieTime { get => _dieTime; set => _dieTime = value; }
    public List<Elite_Skill> SkillStorage { get => _skillStorage; }
    public Elite_Skill CurrentSkill { get => _currentSkill; }
    public List<Elite_Skill> ReadySkills { get => _readySkills; set => _readySkills = value; }
    public float IdleDuration { get => _idleDuration; }
    public Transform HitPoint { get => _hitPoint; }
    public Vector3 ColliderSize { get => _colliderSize; }
    public Transform ParringPoint { get => _parringPoint; }
    public Vector3 ParringColliderSize { get => _parringColliderSize; }
    public CreatePlatform CreatePlatform { get => _createPlatform; }
    #endregion

    protected override void Init()
    {
        _player = FindObjectOfType<Player>().transform;

        _stateStroage.Add(Define.EliteMonsterState.IDLE, new Elite_Idle(this));
        _stateStroage.Add(Define.EliteMonsterState.USESKILL, new Elite_UseSkill(this));
        _stateStroage.Add(Define.EliteMonsterState.GROGGY, new Elite_Groggy(this));
        _stateStroage.Add(Define.EliteMonsterState.FAIL, new Elite_Fail(this));
        _stateStroage.Add(Define.EliteMonsterState.DIE, new Elite_Die(this));

        _stat.Init();
    }

    public void Enter()
    {

        foreach (Elite_Skill s in _skillStorage)
        {
            s.Init(this);
        }

        _currentState = Define.EliteMonsterState.NONE;

        ChangeCurrentState(Define.EliteMonsterState.IDLE);
    }

    public void Stay()
    {
        if (_currentState != Define.EliteMonsterState.NONE)
        {
            _stateStroage[_currentState]?.Stay();
        }
    }

    public void ChangeCurrentState(Define.EliteMonsterState state)
    {

        if (_currentState != Define.EliteMonsterState.NONE)
        {
            _stateStroage[_currentState]?.Exit();
        }
        _currentState = state;

        if (_currentState != Define.EliteMonsterState.NONE)
        {
            _stateStroage[_currentState]?.Enter();
        }

    }


    #region ��ų

    // ��ų�� ������ �� ����ϴ� �Լ�
    public void FinishSkill(Define.EliteMonsterState state = Define.EliteMonsterState.IDLE)
    {
        ChangeCurrentState(state);

        OnFinishSkill = null;
        OnAttackAction = null;
    }

    // ���� ��ų ��ü �Լ�
    public void ChangeCurrentSkill(Define.EliteMonsterSkill skill)
    {
        if (_currentSkill != null)
        {
            _skillStorage.Add(_currentSkill); // ���� ����ҷ� �̵�     
            _currentSkill?.Exit();
        }

        if (skill == Define.EliteMonsterSkill.NONE)
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
    public void ChangeCurrentSkill(Elite_Skill skill)
    {
        if (_currentSkill != null)
        {
            _skillStorage.Add(_currentSkill); // ���� ����ҷ� �̵�     
            _currentSkill?.Exit();
        }
        _currentSkill = skill;
        _currentSkill?.Enter();
    }

    public void ReadySkill(Define.EliteMonsterSkill skill)
    {
        GetSkill(skill).IsCompleted = true;
    }

    // ��ų ã�� �Լ�
    public Elite_Skill GetSkill(Define.EliteMonsterSkill skill) 
    {
        foreach (Elite_Skill s in _skillStorage)
        {
            if (s.Info.skill == skill)
            {              
                return s;
            }
        }

        return null;
    }

    public Elite_Skill GetReadySkill(Define.EliteMonsterSkill skill)
    {
        foreach (Elite_Skill s in _readySkills)
        {
            if (s.Info.skill == skill)
            {
                return s;
            }
        }

        return null;
    }

    #endregion



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_hitPoint.position, _colliderSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_parringPoint.position, _parringColliderSize);
    }
}
