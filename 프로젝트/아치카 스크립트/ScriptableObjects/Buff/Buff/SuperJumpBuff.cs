using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SuperJump", menuName = "ScriptableObjects/Buff Data/Super Jump")]
public class SuperJumpBuff : BuffData
{
    private float _originValue; // ���� ���ݷ� ��

    public override void Enter()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        // ������ ����
        _originValue = _player.PlayerSt.JumpForce;
        _player.PlayerSt.JumpForce = value;
    }

    public override void Stay()
    {
      
    }

    public override void Exit()
    {
        _player.PlayerSt.JumpForce = _originValue; // ������ ����
    }
}
