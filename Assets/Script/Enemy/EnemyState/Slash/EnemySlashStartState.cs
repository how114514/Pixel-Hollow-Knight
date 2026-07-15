using System;
using UnityEngine;
using UnityEngine.InputSystem;

//��ɨǰҡ
public class EnemySlashStartState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemySlashStartState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.FacePlayer();
        enemy.rb.linearVelocity = new Vector2(0, 0);
        enemy.anim.Play("slashStart");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}