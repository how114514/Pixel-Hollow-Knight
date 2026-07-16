using System;
using UnityEngine;

//����
public class EnemyFallState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyFallState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.rb.linearVelocity = new Vector2(0, 0);
        enemy.anim.Play("fall");
    }

    public void OnUpdate()
    {
        /*if (enemy.physicsCheck.isGround)
        { 
            float r = UnityEngine.Random.value;

            if (r < 0.2f)
            {
                stateMachine.ChangeState(enemy.moveState);
            }
            else if (r < 0.6f)
            {
                stateMachine.ChangeState(enemy.backJumpState);
            }
            else
            {
                stateMachine.ChangeState(enemy.jumpState);
            }
        }*/
    }

    public void OnExit()
    {
        enemy.PlaySFX(enemy.landClip);
    }
}