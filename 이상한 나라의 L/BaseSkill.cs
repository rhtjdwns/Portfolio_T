using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseSkill : MonoBehaviour
{
    [Header("Skill Info")]
    protected int SkillId;
    [SerializeField] protected float Damage;                // ������
    [SerializeField] protected float Speed;                 // �ӵ�
    [SerializeField] protected float duration;              // ���� �ð�
    protected float scope;                                  // ���� ����

    protected Vector2 mouseDirection;                       // ���콺 ����

    protected virtual void Awake()
    {
    }

    protected virtual void LifeDuration()
    {
        duration -= Time.deltaTime;
    }

    public virtual void SetDamage(float damage)
    {
        Damage += damage;
    }
}
