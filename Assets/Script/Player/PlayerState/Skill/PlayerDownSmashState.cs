using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownSmashState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerDownSmashState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), true);
        player.rb.linearVelocity = new Vector2(0, -10);
        player.anim.Play("downSmash");
        player.PlaySFX(player.downSmashClip);
    }

    public void OnUpdate()
    {
        if (player.physicsCheck.isGround)
        {
            player.stateMachine.ChangeState(player.downSmashEndState);
        }
    }

    public void OnExit()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), false);
    }
}