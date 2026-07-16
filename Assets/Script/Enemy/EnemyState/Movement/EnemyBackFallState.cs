using System;
using UnityEngine;

//����
public class EnemyBackFallState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyBackFallState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.rb.linearVelocity = new Vector2(0, 0);
        enemy.anim.Play("backFall");
    }

    public void OnUpdate()
    {
        /*if (enemy.physicsCheck.isGround)
        {
            float r = UnityEngine.Random.value;

            if (r < 0.5f)
            {
                stateMachine.ChangeState(enemy.castShockwaveState);
            }
            else
            {
                stateMachine.ChangeState(enemy.chargeStartState);
            }
        }*/
    }

    public void OnExit()
    {
        enemy.PlaySFX(enemy.landClip);
    }
}