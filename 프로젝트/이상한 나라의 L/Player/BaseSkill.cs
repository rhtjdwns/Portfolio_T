using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseSkill : MonoBehaviour
{
    [Header("Skill Info")]
    protected int SkillId;
    [SerializeField] protected float Damage;                // 데미지
    [SerializeField] protected float Speed;                 // 속도
    [SerializeField] protected float duration;              // 지속 시간
    protected float scope;                                  // 공격 범위

    protected Vector2 mouseDirection;                       // 마우스 방향

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
