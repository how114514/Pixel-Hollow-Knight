using System;
using UnityEngine;

//ÏÂÂä¹¥»÷½áÊø
public class EnemyDiveEndState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyDiveEndState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.isDiveAttackEnd = false;
        enemy.anim.Play("diveEnd");
        enemy.PlaySFX(enemy.diveClip);
        enemy.cameraManager.Shake();
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}