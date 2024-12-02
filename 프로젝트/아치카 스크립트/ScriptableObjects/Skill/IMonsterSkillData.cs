using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterSkillData
{
    public Define.SkillTerms SkillTriggerCondition { get; }
    public float SkillTriggerValue { get; }
    public float SkillFiringValue { get; }

    // SkillEffectType
    // 1: �̵� �ӵ�(m/s)
    // 4: ���� ���� ����(cm, 1�� pc��ġ) -> ������ ��������...??
    public float SkillEffectValue2 { get; }

     // 0: None
     // 1: ����
     // 2: �˹�
     // 3: ������
    public Define.SkillEffectType SkillSecondEffectType { get; }

    // SkillEffectType
    // 1: ���� �ð�
    // 2: �˹� �Ÿ�
    public float SkillSecondEffectValue { get; }
}
