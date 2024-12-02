using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUp", menuName = "ScriptableObjects/Buff Data/Speed Up")]
public class SpeedUpBuff : BuffData
{
    [SerializeField] private float _duration;
    private float _timer = 0;

    private float _originValue; // ���� ���ݷ� ��

    public override void Enter()
    {
        _timer = 0;

        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        // �̼� ����
        _originValue = _player.Stat.SprintSpeed;
        _player.Stat.SprintSpeed = value;
    }

    public override void Stay()
    {
        if (_timer >= _duration)
        {
            BuffManager.Instance.RemoveBuff(Define.BuffInfo.SPEEDUP); // ���� ����
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    public override void Exit()
    {
        _player.Stat.SprintSpeed = _originValue; // ���ݷ� ����
    }
}
