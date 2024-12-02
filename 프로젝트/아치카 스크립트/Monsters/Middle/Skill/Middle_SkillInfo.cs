using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Middle_SkillInfo
{
    public Define.MiddleMonsterSkill skill;

    [Header("��Ÿ��")]
    public float coolTime;

    [Header("��Ÿ� (��ä�� ����X)")]
    public float range;

    [Header("������")]
    public float damage;

    [Header("��ų �켱����")]
    public int priority;

    [Header("������ ����")]
    public int level;
}