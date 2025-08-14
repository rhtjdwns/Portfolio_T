using UnityEngine;
using Unity.Netcode;

public class BaseStat : ScriptableObject
{
    [Header("Unit Code")]
    [SerializeField] private string unitCode = "";
    public string UnitCode => unitCode;

    [Header("Unit Name")]
    [SerializeField] private string unitName = "";
    public string UnitName => unitName;

    [Header("Unit Type")]
    [SerializeField] private Define.UnitType unitType = Define.UnitType.NONE;
    public Define.UnitType UnitType => unitType;

    [Header("Unit Stat")]
    [SerializeField] private float maxHp = 100;
    public float MaxHp { get => maxHp; set => maxHp = value; }

    [SerializeField] private float moveSpeed = 5f;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField] private float attackDamage = 1f;
    public float AttackDamage { get => attackDamage; set => attackDamage = value; }

    [SerializeField] private float attackDelay = 1f;
    public float AttackDelay { get => attackDelay; set => attackDelay = value; }

    [SerializeField] private int bossGrade = 1;
    public int BossGrade { get => bossGrade; set => bossGrade = value; }
}
