using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [Header("Weapon Info")]
    protected int WeaponId;                                     // 무기 ID
    protected float AttackRange;                                // 공격 범위
    [SerializeField] protected float AttackPoint;               // 공격력
    [SerializeField] protected float AttackSpeed;               // 공격속도
    [SerializeField] protected float ProjectileSpeed;           // 투사체 속도
    [SerializeField] protected GameObject ProjectileObject;     // 투사체 프리팹
    [SerializeField] protected int MaxAttack;                   // 최대 공격 횟수
    protected float Duration;                                   // 지속 시간
    protected int CurrentAttack = 0;                            // 현재 공격 횟수
    protected int MaxOnceItem = 7;                              // 일회성 아이템 종류 갯수

    protected virtual void Awake()
    {
    }

    public float GetAtkSpd()
    {
        return AttackSpeed;
    }

    public int GetMaxItemAmount()
    {
        return MaxOnceItem;
    }
}
