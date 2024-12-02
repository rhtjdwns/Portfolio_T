using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FAttack", menuName = "ScriptableObjects/EliteMonster/Skill/FAttack", order = 1)]
public class Elite_FAttack : Elite_Skill
{
    private float _coolTime;

    [SerializeField] private float _parringTime; // �и� �ð�

    public override void Init(EliteMonster monster)
    {
        base.Init(monster);


        _coolTime = 0;
    }

    public override void Check()
    {
        if (IsCompleted) return;

        if (_coolTime >= _info.coolTime) // ��Ÿ�� Ȯ��
        {
            if (Vector2.Distance(_monster.Player.position, _monster.transform.position) <= _info.range) // �Ÿ� Ȯ��
            {
                IsCompleted =  true;
            }
        }
        else
        {
            _coolTime += Time.deltaTime;
        }

    }

    public override void Enter()
    {
        //_monster.Ani.SetBool("FAttack", true);

        Debug.Log("�Ϲ� ����1");

        _monster.OnAttackAction += Hit;
        _monster.OnFinishSkill += Finish;
    }
    public override void Stay()
    {
        if (!_monster.Ani.GetBool("FAttack"))
        {
            _monster.Ani.SetBool("FAttack", true);
        }
    }

    public override void Exit()
    {
        _monster.Ani.SetBool("FAttack", false);

        _coolTime = 0;

        IsCompleted = false;
    }

    private bool CheckParringBox()
    {
        return Physics.CheckBox(_monster.ParringPoint.position, _monster.ParringColliderSize / 2, _monster.ParringPoint.rotation, _monster.PlayerLayer);
    }

    // ���� �Լ�
    private void Hit()
    {
        Collider[] hitPlayer = Physics.OverlapBox(_monster.HitPoint.position, _monster.ColliderSize / 2, _monster.HitPoint.rotation, _monster.PlayerLayer);

        foreach (Collider player in hitPlayer)
        {
            if (player.GetComponent<Player>().IsInvincible) return;

            Debug.Log("�Ϲ� ����1 ����");
            float damage = _monster.Stat.Damage * (_info.damage / 100);
            _monster.Player.GetComponent<Player>().TakeDamage(damage);

            // ��Ʈ ��ƼŬ ����
            GameObject hitParticle = ObjectPool.Instance.Spawn("FX_EliteAttack", 1); ;

            Vector3 hitPos = player.ClosestPoint(_monster.HitPoint.position);
            hitParticle.transform.position = new Vector3(hitPos.x, hitPos.y, hitPos.z - 0.1f);
        }
     
    }

    private void Finish()
    {
        _monster.FinishSkill();
    }

}