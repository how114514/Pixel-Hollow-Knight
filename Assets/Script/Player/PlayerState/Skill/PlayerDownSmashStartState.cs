using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownSmashStartState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerDownSmashStartState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.currentEnergy -= 3;
        player.currentEnergy = Mathf.Clamp(player.currentEnergy, 0, player.maxEnergy);
        player.energyUI.SetEnergy(player.currentEnergy);

        player.rb.linearVelocity = new Vector2(0, 10);
        player.anim.Play("downSmashStart");
    }

    public void OnUpdate()

    {
    }

    public void OnExit()
    {
        
    }
}
