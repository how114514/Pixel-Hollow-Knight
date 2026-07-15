using System;
using UnityEngine;

//��
public class EnemyChargeState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyChargeState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.isCharge = true;
        enemy.rb.gravityScale = 0;
        enemy.transform.position += new Vector3(0, enemy.chargeHeight, 0);
        enemy.anim.Play("charge");
        enemy.PlaySFX(enemy.chargeClip);
        enemy.cameraManager.Shake();
    }

    public void OnUpdate()
    {
        enemy.rb.linearVelocity = new Vector2(enemy.chargeSpeed * -enemy.facingDir, 0);

        if (enemy.facingDir == -1 && enemy.physicsCheck.touchRightWall)
        {
            stateMachine.ChangeState(enemy.chargeEndState);
        }

        if (enemy.facingDir == 1 && enemy.physicsCheck.touchLeftWall)
        {
            stateMachine.ChangeState(enemy.chargeEndState);
        }
    }

    public void OnExit()
    {
        enemy.isCharge = false;
        enemy.rb.gravityScale = 2;
    }
}