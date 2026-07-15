using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerHealState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.inputControl.GamePlay.Heal.canceled += HealStopInput;

        player.rb.linearVelocity = new Vector2(0, 0);
        player.anim.Play("heal");
        player.PlaySFX(player.healClip);
    }

    public void OnUpdate()
    {
        StopHeal();
    }

    public void OnExit()
    {
        player.inputControl.GamePlay.Heal.canceled -= HealStopInput;
    }

    private void HealStopInput(InputAction.CallbackContext context)
    {
        player.isPressedHeal = false;
    }

    private void StopHeal()
    {
        if(!player.isPressedHeal)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
