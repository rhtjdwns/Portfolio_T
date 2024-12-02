using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MonsterSkillSlot : SkillSlot
{
    [SerializeReference] public SkillRunnerBase skillRunner;

    public void SetSkill()
    {
        if(Skill != null && Skill is MonsterSkill monsterSkill)
        {
            if(monsterSkill.SkillRunner == skillRunner) { return; }
        }

        Skill = new MonsterSkill(skillRunner);
    }

    public bool IsUsable(MonsterSkillManager sm)
    {
        // �� ��� ���̸� ���� ó��
        var curSkill = Skill as MonsterSkill;
        if(curSkill.IsSkillUsable()) { return false; } 

        // ������ ������ ���� ó��
        var condition = curSkill.skillData.SkillTriggerCondition;
        if (condition == Define.SkillTerms.NONE) { return true; }

        // Ÿ���� ���ٰ� �νĵǸ� ���� ó��
        List<GameObject> targets = GetTargets(sm);
        if(targets.Count == 0) { return false; }

        List<GameObject> objsInRange;
        float radius = curSkill.skillData.SkillTriggerValue * SkillData.cm2m; // cm to m

        // ���� ���� �ִ� obj ����Ʈ ���
        objsInRange = targets.Where(
            (obj) =>
            Mathf.Abs(Vector3.Distance(obj.transform.position, sm.transform.position)) <= radius
        ).ToList();

        // ���ǿ� ���� ��ȯ
        switch (condition)
        {
            case Define.SkillTerms.INRANGE:
                return objsInRange.Count() > 0;    // ���� ���� �ִ� �ֵ��� �ִٸ� true
            case Define.SkillTerms.OUTRANGE:
                return objsInRange.Count() == 0;   // ���� ���� �ִ� �ֵ��� ���ٸ� true
            default:
                return false;
        }
    }

    private List<GameObject> GetTargets(MonsterSkillManager sm)
    {
        var curSkill = Skill as MonsterSkill;
        List<GameObject> targets;
        switch (curSkill.skillData.SkillCastingTarget)
        {
            case Define.SkillTarget.PC:
                targets = CharacterManager.Instance.GetCharacter(1 << 11); // player layer
                break;
            case Define.SkillTarget.SELF:
                targets = new List<GameObject>
                {
                    sm.gameObject
                };
                break;
            case Define.SkillTarget.MON:
                targets = CharacterManager.Instance.GetCharacter(1 << 10); // monster layer
                break;
            case Define.SkillTarget.GROUND:
                targets = new List<GameObject>();

                if (Physics.Raycast(new Ray(sm.transform.position, Vector3.down), out RaycastHit hit, 100, 1 << 12))
                {
                    targets.Add(hit.transform.gameObject);
                }
                break;
            case Define.SkillTarget.ALL:
                targets = CharacterManager.Instance.GetCharacter(1 << 11 | 1 << 10);
                break;
            case Define.SkillTarget.NONE:
            default:
                targets = new List<GameObject>();
                break;
        }

        return targets;
    }

    public override void UseSkillInstant(CharacterBase character, UnityAction OnEnded = null)
    {
        if (Skill == null) { return; }

        Skill.UseSkill(character, OnEnded);
    }
}
