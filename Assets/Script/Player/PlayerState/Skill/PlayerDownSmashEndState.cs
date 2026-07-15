using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownSmashEndState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerDownSmashEndState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), true);
        player.anim.Play("downSmashEnd");
        player.PlaySFX(player.downSmashLandClip);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), false);
    }
}