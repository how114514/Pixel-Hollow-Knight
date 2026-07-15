using System;
using UnityEngine;

//°×²¨
public class EnemyCastShockwaveState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyCastShockwaveState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.anim.Play("castShockwave");
        enemy.PlaySFX(enemy.shockwaveClip);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}