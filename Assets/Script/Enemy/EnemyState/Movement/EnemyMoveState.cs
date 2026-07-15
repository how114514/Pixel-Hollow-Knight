using System;
using UnityEngine;
using UnityEngine.InputSystem;

//�ƶ�
public class EnemyMoveState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyMoveState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.FacePlayer();
        enemy.anim.Play("move");
    }

    public void OnUpdate()
    {
        MoveToPlayer();
    }

    public void OnExit()
    {

    }

    private void MoveToPlayer()
    {
        float distance = Mathf.Abs(enemy.player.position.x - enemy.transform.position.x);

        if (distance <= enemy.attackDistance)
        {
            enemy.rb.linearVelocity = new Vector2(0, 0);

            stateMachine.ChangeState(enemy.slashStartState);
            return;
        }
        if (enemy.isStartGame)
        {
            enemy.rb.linearVelocity = new Vector2(enemy.moveSpeed * -enemy.facingDir, 0);
        }
    }
}