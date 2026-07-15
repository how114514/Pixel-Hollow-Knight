using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeafenedState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;
    float deafenedTimer = 1f;

    public PlayerDeafenedState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.rb.linearVelocity = Vector2.zero;
        player.anim.Play("deafened");
    }

    public void OnUpdate()
    {
        deafenedTimer -= Time.deltaTime;

        if (deafenedTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public void OnExit()
    {
        
    }
}
