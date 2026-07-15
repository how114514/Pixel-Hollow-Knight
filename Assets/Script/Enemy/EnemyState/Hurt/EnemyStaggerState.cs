using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaggerState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyStaggerState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.staggerTimer = enemy.staggerTime;
        enemy.rb.linearVelocity = new Vector2(0, 0);
        enemy.anim.Play("stagger");
        enemy.PlaySFX(enemy.staggerClip);
        enemy.HurtShake();
        enemy.Knockback(enemy.playerPos);
    }

    public void OnUpdate()
    {
        enemy.staggerTimer -= Time.deltaTime;

        if (enemy.staggerTimer <= 0f)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public void OnExit()
    {

    }
}