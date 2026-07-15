using System;
using UnityEngine;

//≥Â«∞
public class EnemyChargeStartState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyChargeStartState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.anim.Play("chargeStart");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}