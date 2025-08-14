using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : IState
{
    private Enemy enemy;

    public EnemyMoveState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter() 
    {
        enemy.SetAnimation(true, false);
    }

    public void Stay()
    {
        if (!enemy.HasValidTarget())
        {
            Debug.Log("No valid target");
            return;
        }

        Vector3 targetPosition = enemy.GetTargetPosition();
        Vector3 dir = targetPosition - enemy.transform.position;
        float distanceToTarget = enemy.GetDistanceToTarget();

        if (distanceToTarget < 2f)
        {
            Debug.Log("Attack");
            enemy.stateMachine.ChangeState(new EnemyAttackState(enemy));
            return;
        }

        dir.Normalize();

        // 서버에서만 실제 이동 처리
        if (enemy.IsServer)
        {
            enemy.Rb.MovePosition(enemy.Rb.position + dir * enemy.Stat.MoveSpeed * Time.deltaTime);
        }

        // 모든 클라이언트에서 회전 처리 (시각적 효과)
        enemy.transform.rotation = Quaternion.Lerp(
            enemy.transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * 10f
        );
    }

    public void FixedStay() {}

    public void Exit()
    {
        enemy.SetAnimation(false, false);
    }
}
