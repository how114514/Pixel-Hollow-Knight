using System;
using UnityEngine;
using UnityEngine.InputSystem;

//�����
public class EnemyJumpState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyJumpState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.FacePlayer();

        enemy.anim.Play("jump");
        enemy.PlaySFX(enemy.jumpClip);

        float distance = enemy.player.position.x - enemy.transform.position.x;

        float dir = Mathf.Sign(distance);

        enemy.rb.linearVelocity = new Vector2(dir * Mathf.Abs(distance), enemy.jumpSpeed);
    }

    public void OnUpdate()
    {
        ChangeToDiveStart();
    }

    public void OnExit()
    {
        enemy.fallByAttack = false;
    }

    private void ChangeToDiveStart()
    {
        if (enemy.rb.linearVelocity.y < -0.1)
        {
            stateMachine.ChangeState(enemy.diveStartState);
        }
    }
}