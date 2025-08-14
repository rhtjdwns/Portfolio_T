using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState
{
    private Enemy enemy;

    public EnemyIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void Enter()
    {
    }

    public void Stay()
    {
        if (enemy.HasValidTarget())
        {
            enemy.stateMachine.ChangeState(new EnemyMoveState(enemy));
            return;
        }
    }

    public void FixedStay()
    {

    }

    public void Exit()
    {

    }
}
