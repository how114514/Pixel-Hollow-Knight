using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeadState : EnemyIState
{
    EnemyController enemy;
    EnemyStateMachine stateMachine;

    public EnemyDeadState(EnemyController enemy, EnemyStateMachine machine)
    {
        this.enemy = enemy;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        enemy.anim.Play("dead");
        enemy.PlaySFX(enemy.deadExplosionStartClip);
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