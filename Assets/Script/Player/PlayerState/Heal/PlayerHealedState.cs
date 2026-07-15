using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealedState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerHealedState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.inputControl.GamePlay.Heal.canceled += HealStopInput;
        player.anim.Play("healed");
        player.PlaySFX(player.healedClip);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        player.inputControl.GamePlay.Heal.canceled -= HealStopInput;
    }

    private void HealStopInput(InputAction.CallbackContext context)
    {
        player.isPressedHeal = false;
    }
}
