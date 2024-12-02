using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    [Header("�ı� ���� ����")]
    [SerializeField] protected bool isDestory;
    [Header("�ִ� ü��")]
    [SerializeField] protected float maxHp;
    [Header("���� ü��")]
    private float _curHp;

    [Space]

    [Header("�ִϸ��̼�")]
    private Animator _animator;

    public Animator Ani {  get { return _animator; } }
    public float Hp { get { return _curHp; } }

    protected virtual void Awake()
    {
        TryGetComponent<Animator>(out _animator);

        _curHp = maxHp;
    }

    public virtual void TakeDamage(float value)
    {
        if (!isDestory)
        {
            return;
        }

        _curHp -= value;
    }
}
