using System;
using UnityEngine;

//���乥��׼��
public class EnemyDiveStartState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyDiveStartState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.isDiveStart = true;
        enemy.rb.gravityScale = 0;
        enemy.anim.Play("diveStart");
    }

    public void OnUpdate()
    {
        enemy.rb.linearVelocity = new Vector2(0, 0);
    }

    public void OnExit()
    {
        enemy.isDiveStart = false;
        enemy.rb.gravityScale = 2;
    }
}