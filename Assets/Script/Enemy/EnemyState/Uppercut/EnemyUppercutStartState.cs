using System;
using UnityEngine;
using UnityEngine.InputSystem;

//����ǰҡ
public class EnemyUppercutStartState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyUppercutStartState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.rb.linearVelocity = new Vector2(0, 0);
        enemy.anim.Play("uppercutStart");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}