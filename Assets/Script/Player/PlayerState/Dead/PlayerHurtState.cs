using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHurtState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    int playerLayer;
    int enemyLayer;

    public PlayerHurtState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.isHurt = true;
        player.isInvincible = true;

        player.anim.Play("hurt");
        player.PlaySFX(player.hurtClip);

        player.invincibleTimer = player.invincibleTime;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), true);

        player.cameraManager.Shake();
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        player.isHurt = false;
    }
}