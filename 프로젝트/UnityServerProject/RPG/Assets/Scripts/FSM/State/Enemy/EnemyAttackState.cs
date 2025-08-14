using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState
{
    private Enemy enemy;
    private float time = 0f;

    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter() 
    {
        enemy.SetAnimation(false, true);
    }

    public void Stay()
    {
        time += Time.deltaTime;
        if (time > enemy.Stat.AttackDelay)
        {
            time = 0;

            if (!enemy.HasValidTarget())
            {
                Debug.Log("No valid target");
                enemy.stateMachine.ChangeState(new EnemyIdleState(enemy));
                return;
            }

            Vector3 targetPosition = enemy.GetTargetPosition();
            Vector3 dir = targetPosition - enemy.transform.position;
            float distanceToTarget = enemy.GetDistanceToTarget();

            if (distanceToTarget >= 2f)
            {
                Debug.Log("[Enemy] Change Move");
                enemy.stateMachine.ChangeState(new EnemyMoveState(enemy));
                return;
            }
        }
    }

    public void FixedStay() {}

    public void Exit()
    {
        enemy.SetAnimation(false, false);
    }
}
