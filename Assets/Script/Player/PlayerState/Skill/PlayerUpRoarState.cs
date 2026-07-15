using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpRoarState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerUpRoarState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }
    public void OnEnter()
    {
        player.PlaySFX(player.upRoarClip);
        player.anim.Play("upRoar");
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