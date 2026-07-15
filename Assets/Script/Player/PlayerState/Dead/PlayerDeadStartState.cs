using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadStartState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerDeadStartState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }
    public void OnEnter()
    {
        player.rb.linearVelocity = new Vector2(0, 0);

        player.anim.Play("deadStart");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
