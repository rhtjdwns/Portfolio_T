using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Barrier", menuName = "ScriptableObjects/EliteMonster/Skill/Barrier", order = 1)]
public class Elite_Barrier : Elite_Skill
{
    private float _coolTime;
    private float _totalTime;

    [SerializeField] private float _defense;   // ������ ���ҷ�

    private float _lastHp;

    private GameObject _guardEffect;

    public override void Init(EliteMonster monster)
    {
        base.Init(monster);

        _coolTime = 0;
        _totalTime = 0;
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

        Debug.Log("����");
        _lastHp = _monster.Stat.Hp;
        _monster.Stat.Defense = _defense;

        // ���� ����Ʈ ����
        _guardEffect = ObjectPool.Instance.Spawn("FX_EliteGuard");
        _guardEffect.transform.position = _monster.transform.position;

        _monster.GetComponent<SphereCollider>().enabled = true;

        _monster.IsGuarded = true;
    }
    public override void Stay()
    {
        if (!_monster.Ani.GetBool("Guard"))
        {
            _monster.Ani.SetBool("Guard", true);
        }

        if (_totalTime >= _info.totalTime)
        {
            if (_lastHp > _monster.Stat.Hp) // �÷��̾ ���� ������ ���� ���� ��(ü�� ��ȭ�� ���� ��)
            {
                _monster.ChangeCurrentSkill(Define.EliteMonsterSkill.SUPERPUNCH); // �ָ� ġ�� ����
                IsCompleted = false;
            }
            else
            {
                _monster.FinishSkill();
            }
            Debug.Log("���� ��");
        }
        else
        {
            _totalTime += Time.deltaTime;
        }
    }
    public override void Exit()
    {
        _monster.Ani.SetBool("Guard", false);

        _monster.GetComponent<SphereCollider>().enabled = false;

        // ���� ����Ʈ ����
        ObjectPool.Instance.Remove(_guardEffect);
      

        _monster.Stat.Defense = 0;
        _totalTime = 0;
        _coolTime = 0;

        _monster.IsGuarded = false;
        IsCompleted = false;
    }

}
