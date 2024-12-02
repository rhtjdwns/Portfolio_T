using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerAttack
{
    #region ����
    private Player _player;

    private Define.AttackState _currentAttackState;
    private Dictionary<Define.AttackState, PlayerAttackState> _attackStateStorage;

    private Queue<TempoAttackData> _mainTempoQueue;
    private TempoAttackData _currentTempoData;

    //public TempoCircle PointTempoCircle { get; set; }

    // �̺�Ʈ
    public bool IsHit { get; set; }
    public float CheckDelay { get; set; } // üũ ���� ���� �ð�
    public bool isAttack {  get; set; }
    #endregion

    #region ������Ƽ

    public Define.AttackState CurrentAttackkState { get => _currentAttackState; }
  
    public TempoAttackData CurrentTempoData { get=> _currentTempoData; }// ���� ���� ������ 
    #endregion

    public PlayerAttack(Player player)
    {
        _player = player;
    }

    public void Initialize()
    {
        _currentAttackState = Define.AttackState.FINISH;
        _attackStateStorage = new Dictionary<Define.AttackState, PlayerAttackState>();

        _mainTempoQueue = new Queue<TempoAttackData>();
        _currentTempoData = null;

        IsHit = false;
        isAttack = true;

        CheckDelay = 0;

        //�÷��̾� ���� ����
        _attackStateStorage.Add(Define.AttackState.ATTACK, new AttackState(_player));
        _attackStateStorage.Add(Define.AttackState.CHECK, new CheckState(_player));
        _attackStateStorage.Add(Define.AttackState.FINISH, new FinishState(_player));

        foreach (var storage in _attackStateStorage)
        {
            storage.Value.Initialize();
        }

        ResetMainTempoQueue();
    }

    public void Update()
    {
        // ����ȭ üũ
        //if (_player.CheckOverload()) 
        //{
        //    if (_player.CurrentState == Define.PlayerState.NONE)
        //    {
        //        _player.CurrentState = Define.PlayerState.OVERLOAD;
        //    }
        //}

        if (isAttack || _player.Controller.isJump)
        {
            if (_currentAttackState != Define.AttackState.ATTACK && _player.Ani.GetInteger("CommandCount") == 0)
            { 
                // ���� Ű �Է�
                if (PlayerInputManager.Instance.attack)
                {
                    PlayerInputManager.Instance.attack = false;
                    if (_player.Ani.GetBool("isGrounded"))
                    {
                        AttackMainTempo();
                    }
                }
            }
            else
            {
                if (PlayerInputManager.Instance.attack || PlayerInputManager.Instance.keyX)
                {
                    PlayerInputManager.Instance.attack = false;
                    PlayerInputManager.Instance.keyX = false;
                }
            }
        }

        _attackStateStorage[_currentAttackState]?.Stay();
    }

    public void ChangeCurrentAttackState(Define.AttackState state)
    {
        _attackStateStorage[_currentAttackState]?.Exit();
        _currentAttackState = state;
        _attackStateStorage[_currentAttackState]?.Enter();
    }


    #region ���� ����
    public void AttackMainTempo() // ���� ����
    {
        if (_player.Ani.GetBool("isGrounded") && _mainTempoQueue.Count > 0)
        {
            TestSound.Instance.PlaySound("SmashSwing");
            _currentTempoData = _mainTempoQueue.Dequeue();

            ChangeCurrentAttackState(Define.AttackState.ATTACK);

            // ť�� ��������� �ʱ�ȭ
            if (_mainTempoQueue.Count == 0)
            {
                ResetMainTempoQueue();
            }
        }
    }

    public void ResetMainTempoQueue()
    {
        _mainTempoQueue.Clear();

        foreach (TempoAttackData data in _player.MainTempoAttackDatas)
        {
            _mainTempoQueue.Enqueue(data);
        }
    }

    public void SetMainTempoQueue(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _mainTempoQueue.Dequeue();
        }
    }

    #endregion
}
