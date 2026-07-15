using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleCryStartState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyBattleCryStartState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
        enemy.anim.Play("battleCryStart");
    }

    public void OnUpdate()
    {
        if (enemy.physicsCheck.isGround)
        {
            stateMachine.ChangeState(enemy.battleCryState);
        }
    }

    public void OnExit()
    {

    }
}