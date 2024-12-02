using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "StompRunner", menuName = "ScriptableObjects/Skill/Runner/StompRunner", order = 1)]
public class StompRunner : SkillRunnerBase
{
    public override IEnumerator SkillCoroutine(CharacterBase character)
    {
        Rigidbody rigid = character.Rb;
        rigid.useGravity = false;

        float movingDistance = skillData.SkillEffectValue * SkillData.cm2m;

        var targets = GetTargets(skillData.SkillCastingTarget, character);

        // ����
        yield return preDelayWFS;

        Vector3 initialPos = character.transform.position;
        Vector3 targetPos = targets[0].transform.position;
        Vector3 direction = (targetPos - initialPos);
        direction.y = 0f;
        direction.Normalize();

        // ���� �� ����
        float curTime = 0;
        float regenTime = skillData.SkillRegenTime * SkillData.Time2Second;

        List<CharacterBase> hittedCharacters = new List<CharacterBase>();

        // �ݶ��̴� ������ ��������
        float halfColliderSize = character.ColliderManager.GetHalfSizeForMain(Vector3.right);
        float colliderSizeForTarget = 0;
        var target = GetCharacter(targets[0]);
        if(target != null)
        {
            colliderSizeForTarget = target.ColliderManager.GetHalfSizeForMain(Vector3.right) * 2f;
        }

        // ��Ʈ�ڽ�
        character.ColliderManager.SetActiveCollider(false, Define.ColliderType.PERSISTANCE);

        // ��ǥ ��ġ ���
        targetPos = GetTargetPosByCoillision(initialPos, direction, targetPos, 1 << 13, halfColliderSize + colliderSizeForTarget);

        if (character.transform.position.x - targets[0].transform.position.x > 0)
        {
            character.CharacterModel.localScale = new Vector3(-1, 1, 1);
        }
        else if (character.transform.position.x - targets[0].transform.position.x < 0)
        {
            character.CharacterModel.localScale = new Vector3(1, 1, 1);
        }

        TestSound.Instance.PlaySound("NormalMonster2_Skill");

        // ������ � ����
        while (curTime <= regenTime)
        {
            yield return null;

            curTime += Time.deltaTime;
            float completeRatio = curTime / regenTime;

            /// �ְ� ���̱��� ������ ���Ŀ� �浹 ó���� ����
            //if(completeRatio > 0.5f)
            //{
            //    Vector3 rayOrigin = character.transform.position; // ������ Bottom�� �������� �ؾ� �Ʒ��� �� �� ����.
            //    Ray ray = new Ray(rayOrigin, Vector3.down);
            //    Debug.DrawRay(rayOrigin, Vector3.down, Color.blue);
            //    float collisiionDepth = skillData.SkillHitboxSize * SkillData.cm2m;
            //    int layerMask = SkillTargetToLayerMask(skillData.SkillCastingTarget);
            //    if (Physics.Raycast(ray, out RaycastHit characterHit, collisiionDepth, layerMask))
            //    {
            //        hittedCharacters.Add(characterHit.transform.GetComponent<CharacterBase>());
            //    }
            //}

            Vector3 nextPos = EvaluateParabola(initialPos, targetPos, 3, completeRatio); // 3�� �ӽ� �� ���� ���� ������ ���� �ʿ�
            character.transform.position = nextPos;
        }

        yield return new WaitForSeconds(0.1f);

        NormalMonster monster = character.GetComponent<NormalMonster>();
        Vector3 originScale = monster.HitPoint.localScale;

        monster.HitPoint.localScale = new Vector3(3, 3, 1);
        Collider[] hittedCharacter = Physics.OverlapBox(monster.HitPoint.position, monster.HitPoint.localScale / 2, monster.HitPoint.rotation, monster.PlayerLayer);
        monster.HitPoint.localScale = originScale;

        foreach (var hitCharacter in hittedCharacter)
        {
            hitCharacter.GetComponent<CharacterBase>().TakeDamage(skillData.SkillDamage);
        }

        // ĳ���� Ÿ��
        //foreach (var hittedCharacter in hittedCharacters.Distinct())
        //{
        //    float damageAmount = skillData.SkillDamage * character.Stat.Damage;

        //    hittedCharacter.TakeDamage(damageAmount);
        //}

        var targetsInRanged = Physics.SphereCastAll(character.transform.position, 5, Vector3.up, 0, SkillTargetToLayerMask(skillData.SkillCastingTarget));
        TestSound.Instance.PlaySound("NormalMonster2_AttackHit");

        GameObject effect = ObjectPool.Instance.Spawn("P_BalkoongMonsterSkill", 1f);
        effect.transform.position = character.transform.position + new Vector3(0, 0.3f);

        foreach (var targetInReanged in targetsInRanged)
        {
            var targetCharacter = targetInReanged.transform.GetComponent<CharacterBase>();
            if (targetCharacter != null)
            { 
                KnockbackTarget(targetCharacter, character, 5);
            }
        }

        // �ʱ�ȭ
        character.transform.position = targetPos;
        rigid.velocity = Vector3.zero;
        character.ColliderManager.SetActiveCollider(true, Define.ColliderType.PERSISTANCE);
        rigid.useGravity = true;
        Debug.Log("Stomp End");
    }

    // ������ ��� �ϰ� ���ִ� �޼ҵ�
    // normalizedTime ���� ���� start���� end������ ������ �� ��� ��ġ�� �ִ����� ��ȯ�Ѵ�.
    private Vector3 EvaluateParabola(Vector3 start, Vector3 end, float height, float normalizedTime)
    {
        float quadratic = -4 * height * normalizedTime * normalizedTime + 4 * height * normalizedTime;

        Vector3 vec = Vector3.Lerp(start, end, normalizedTime);

        return new Vector3(vec.x, quadratic + start.y, start.z);
    }

    private CharacterBase GetCharacter(GameObject obj)
    {
        var character = obj.GetComponent<CharacterBase>();
        if (character == null)
        {
            Debug.LogWarning("This Object has not Character Base");
        }

        return character;
    }

    private void KnockbackTarget(CharacterBase target, CharacterBase self, float power)
    {
        Vector3 selfToTarget = target.transform.position - self.transform.position;
        selfToTarget.y = 0;

        if(selfToTarget == Vector3.zero)
        {
            selfToTarget = self.transform.right * (self.IsLeftDirection() ? -1 : 1);
        }

        Vector3 direction = selfToTarget.normalized;
        Vector3 velocity = direction * power;
        velocity.y = target.Rb.velocity.y;
        target.Rb.velocity = velocity;
    }
}
