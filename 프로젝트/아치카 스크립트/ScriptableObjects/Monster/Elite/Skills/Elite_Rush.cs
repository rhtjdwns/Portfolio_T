using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Rush", menuName = "ScriptableObjects/EliteMonster/Skill/Rush", order = 1)]
public class Elite_Rush : Elite_Skill
{
    private float _coolTime;

    private Tweener _rushTween;

    [SerializeField] private float _rushDistance;
    [SerializeField] private float _rushDuration;
    [SerializeField] AnimationCurve customCurve; // ����� ���� �ִϸ��̼� Ŀ��

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
        _monster.Direction = _monster.Player.position.x - _monster.transform.position.x; // �÷��̾� �ٶ󺸱�

        GameObject rushEffect;
        if (_monster.Direction == 1)
        {
            rushEffect = ObjectPool.Instance.Spawn("FX_EliteRush_R", 0, _monster.transform);
        }
        else
        {
            rushEffect = ObjectPool.Instance.Spawn("FX_EliteRush_L", 0, _monster.transform);
        }

        // ����
        _rushTween = _monster.transform.DOMoveX(_monster.transform.position.x + _rushDistance * _monster.Direction, _rushDuration).SetEase(customCurve).OnUpdate(() =>
        {
            if (CheckHit()) // �÷��̾�� �浹 ��
            {
                _monster.FinishSkill();
                
                ObjectPool.Instance.Remove(rushEffect);
                GameObject rushAttackEffect = ObjectPool.Instance.Spawn("FX_EliteRushAttack", 1);
                rushAttackEffect.transform.position = _monster.transform.position + new Vector3(_monster.Direction, 0, -0.1f);

                _rushTween.Kill();
            }

        }).OnComplete(() => { ObjectPool.Instance.Remove(rushEffect); _monster.FinishSkill();  }); // �̵��� ���� ��

    }
    public override void Stay()
    {
        if (!_monster.Ani.GetBool("Dash"))
        {
            _monster.Ani.SetBool("Dash", true);
        }

    }

    public override void Exit()
    {
        _monster.Ani.SetBool("Dash", false);

        _coolTime = 0;

        IsCompleted = false;
    }

    // �浹 Ȯ�� �Լ�
    private bool CheckHit()
    {
        if (Physics.CheckBox(_monster.HitPoint.position, _monster.ColliderSize / 2, _monster.HitPoint.rotation, _monster.PlayerLayer))
        {
            if (!_monster.Player.GetComponent<Player>().IsInvincible)
            {
                float damage = _monster.Stat.Damage * (_info.damage / 100);
                _monster.Player.GetComponent<Player>().TakeDamage(damage);
            }
            return true;
        }
        else if (Physics.CheckBox(_monster.HitPoint.position, _monster.ColliderSize / 2, _monster.HitPoint.rotation, _monster.WallLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
