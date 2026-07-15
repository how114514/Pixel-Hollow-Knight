using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCastShockwaveState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerCastShockwaveState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }
    public void OnEnter()
    {
        player.anim.Play("castShockwave");
        player.PlaySFX(player.castShockwaveClip);
    }

    public void OnUpdate()
    {
        player.rb.gravityScale = 0;
        player.rb.linearVelocity = new Vector2(0, 0);
    }

    public void OnExit()
    {
        player.rb.gravityScale = 2;
    }
}