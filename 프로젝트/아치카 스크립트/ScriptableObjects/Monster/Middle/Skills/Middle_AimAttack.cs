using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AimAttack", menuName = "ScriptableObjects/MiddleMonster/Skill/AimAttack", order = 1)]
public class Middle_AimAttack : Middle_Skill
{
    [Header("���Ϳ��� ������ ����")]
    [SerializeField] private float monsterDamage;
    [Header("��ź ����")]
    [SerializeField] private int bombAmount;  
    [Header("�ʱ� �̻��� �ӵ�")]
    [SerializeField] private float initSpeed = 10f; 
    [Header("���� ����")]
    [SerializeField] private float strength = 5f;
    [Header("�ִ� �̻��� �ӵ�")]
    [SerializeField] private float missleSpeed = 10f;

    private float _coolTime = 0f;

    public override void Init(MiddleMonster monster)
    {
        base.Init(monster);

        _coolTime = 0;
    }

    public override void Check()
    {
        if (IsCompleted) return;

        if (_coolTime >= _info.coolTime) // ��Ÿ�� Ȯ��
        {
            IsCompleted = true;
        }
        else
        {
            _coolTime += Time.deltaTime;
        }
    }

    public override void Enter()
    {
        Debug.Log("���� �̻��� �߻�");

        _coolTime = 0;

        _monster.OnAttackAction += Attack;
        _monster.OnFinishSkill += Finish;

        TestSound.Instance.PlaySound("AimAttack_Ready");
        TestSound.Instance.PlaySound("AimAttack_Voice");
    }
    public override void Stay()
    {
        if (!_monster.Ani.GetBool("AimAttack"))
        {
            _monster.Ani.SetBool("AimAttack", true);
        }
    }

    public override void Exit()
    {
        if (_monster.Ani.GetBool("AimAttack"))
        {
            _monster.Ani.SetBool("AimAttack", false);
        }

        _coolTime = 0;
    }

    private void Attack()
    {
        TestSound.Instance.PlaySound("AimAttack_Firing");
        TestSound.Instance.PlaySound("AimAttack_Guid");

        GameObject mark = ObjectPool.Instance.Spawn("TraceMark", 0, _monster.Player);
        mark.transform.position = _monster.Player.transform.position + new Vector3(0, 1f, -1);

        GameObject bomb = ObjectPool.Instance.Spawn("TraceRocket").GetComponent<AimRocket>().SettingValue(_monster.Player, Info.damage, monsterDamage, mark, initSpeed, strength, missleSpeed);
        bomb.transform.position = _monster.HitPoint.position + new Vector3(0, 0, -1f);
    }

    private void Finish()
    {
        IsCompleted = false;
        _monster.Ani.SetInteger("AimAttackCount", 0);
        _monster.FinishSkill();
    }
}
