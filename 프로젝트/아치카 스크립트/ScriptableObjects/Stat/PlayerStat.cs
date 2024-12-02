using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "ScriptableObjects/Stat/Player Stat")]
public class PlayerStat : Stat
{

    [Header("����")]
    [SerializeField] private float _jumpForce;// ���� ��

    [Header("�뽬")]
    [SerializeField] private float _dashDelay = 5f;
    [SerializeField] private float _dashDistance = 5f;
    [SerializeField] private float _dashDuration = 0.2f;

    [Header("����")]
    [SerializeField] private float _stunDelay; // ����ȭ �� ���ϱ��� �ɸ��� �ð�
    [SerializeField] private float _stunTime; // ���� ���� �ð�

    [Header("�ñر� ����")]
    [SerializeField] public float _maxUltimateGauge;
    private float _curUltimateGauge;

    [Header("�ǰݽð�")]
    [SerializeField] public float hitTime = 1;

    [Space]

    [Header("Ŀ�ǵ� ����")]
    [SerializeField] private float keyInputTime = 0.2f;

    public bool IsKnockedBack { get; set; } = false;

    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (_hp <= 0)
            {
                _hp = 0;
                _isDead = true;
            }
            else if (_hp > _maxHp)
            {
                _hp = _maxHp;
            }
        }
    }

    public float JumpForce { get => _jumpForce; set => _jumpForce = value; }
    public float DashDelay { get => _dashDelay; }
    public float DashDistance { get => _dashDistance; }
    public float DashDuration { get => _dashDuration; }
    public float StunDelay { get => _stunDelay; }// ���� ���� �ð�
    public float StunTime { get => _stunTime; }// ���� ���� �ð�
    public float CurUltimateGauge { get => _curUltimateGauge; set => _curUltimateGauge = value; }
    public float KeyInputTime { get => keyInputTime; set => keyInputTime = value; }

    public override void Init()
    {
        _hp = _maxHp;
    }

}
