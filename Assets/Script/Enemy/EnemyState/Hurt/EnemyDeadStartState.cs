using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadStartState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyDeadStartState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.isDead = true;
        enemy.rb.linearVelocity = new Vector2(0, 0);
        enemy.anim.Play("deadStart");
        enemy.PlaySFX(enemy.deadClip);
    }

    public void OnUpdate()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerAttack"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerSkill"), LayerMask.NameToLayer("EnemyBody"), true);
    }

    public void OnExit()
    {

    }
}