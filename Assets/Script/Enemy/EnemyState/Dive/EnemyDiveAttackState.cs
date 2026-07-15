using System;
using UnityEngine;

//���乥��
public class EnemyDiveAttackState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyDiveAttackState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.anim.Play("diveAttack");
    }

    public void OnUpdate()
    {
        enemy.rb.linearVelocity = new Vector2(0, -enemy.diveSpeed);
    }

    public void OnExit()
    {

    }
}