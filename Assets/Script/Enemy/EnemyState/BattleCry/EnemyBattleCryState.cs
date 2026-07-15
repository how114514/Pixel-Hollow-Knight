using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleCryState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyBattleCryState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.anim.Play("battleCry");
        enemy.PlaySFX(enemy.battleRoarClip);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        enemy.isStartGame = true;
    }
}