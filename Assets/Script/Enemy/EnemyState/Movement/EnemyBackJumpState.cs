using System;
using UnityEngine;

//����
public class EnemyBackJumpState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyBackJumpState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        float playerX = enemy.player.transform.position.x;

        float distLeft = Mathf.Abs(playerX - enemy.minX);
        float distRight = Mathf.Abs(playerX - enemy.maxX);

        float targetEdge = distLeft > distRight ? enemy.minX : enemy.maxX;

        float dir = Mathf.Sign(targetEdge - enemy.transform.position.x);

        enemy.rb.linearVelocity = new Vector2(dir * enemy.jumpBackSpeedX, enemy.jumpBackSpeedY);
        enemy.anim.Play("backJump");
        enemy.PlaySFX(enemy.jumpClip);
    }

    public void OnUpdate()
    {
        enemy.FacePlayer();

        /*if (enemy.facingDir == 1 && enemy.physicsCheck.touchRightWall)
        {
            stateMachine.ChangeState(enemy.backFallState);
        }

        if (enemy.facingDir == -1 && enemy.physicsCheck.touchLeftWall)
        {
            stateMachine.ChangeState(enemy.backFallState);
        }*/
    }

    public void OnExit()
    {

    }
}