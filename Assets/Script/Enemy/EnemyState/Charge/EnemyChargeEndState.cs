using System;
using UnityEngine;

//³åºó
public class EnemyChargeEndState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyChargeEndState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.anim.Play("chargeEnd");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}