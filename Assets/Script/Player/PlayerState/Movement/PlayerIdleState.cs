using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerIdleState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.inputControl.GamePlay.Jump.started += JumpInput;
        player.inputControl.GamePlay.Dash.started += DashInput;
        player.inputControl.GamePlay.Attack.started += AttackInput;
        player.inputControl.GamePlay.Heal.started += HealInput;
        player.inputControl.GamePlay.Skill.started += SkillInput;
    }

    public void OnUpdate()
    {
        player.anim.Play("idle");

        ChangeToMove();

        ChangeToFall();

        Heal();
    }

    public void OnExit()
    {
        player.inputControl.GamePlay.Jump.started -= JumpInput;
        player.inputControl.GamePlay.Dash.started -= DashInput;
        player.inputControl.GamePlay.Attack.started -= AttackInput;
        player.inputControl.GamePlay.Heal.started -= HealInput;
        player.inputControl.GamePlay.Skill.started -= SkillInput;
    }

    private void SkillInput(InputAction.CallbackContext context)
    {
        if (player.currentEnergy >= 3)
        {
            Vector2 inputDirection = player.inputControl.GamePlay.Move.ReadValue<Vector2>();

            if (inputDirection.y > 0.1f)
            {
                stateMachine.ChangeState(player.upRoarState);
            }
            else if (inputDirection.y < -0.1f)
            {
                stateMachine.ChangeState(player.downSmashStartState);
            }
            else
            {
                stateMachine.ChangeState(player.castShockwaveState);
            }
        }
    }

    private void Heal()
    {
        if (player.isPressedHeal && player.currentEnergy >= 3) 
        {
            stateMachine.ChangeState(player.healState);
        }
    }

    private void HealInput(InputAction.CallbackContext context)
    {
        player.isPressedHeal = true;
    }

    private void AttackInput(InputAction.CallbackContext context)
    {
        ChangeToAttack();
    }

    private void DashInput(InputAction.CallbackContext context)
    {
        if (player.canDash)
        {
            if (player.canBlackDash)
            {
                ChangeToBlackDash();
            }
            else
            {
                ChangeToDash();
            }
        }
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if (player.physicsCheck.isGround)
        {
            ChangeToJump();
        }
    }

    private void ChangeToMove()
    {
        Vector2 inputDirection = player.inputControl.GamePlay.Move.ReadValue<Vector2>();

        if (Mathf.Abs(inputDirection.x) > 0.01f)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }

    private void ChangeToJump()
    {
        stateMachine.ChangeState(player.jumpState);
    }

    private void ChangeToDash()
    {
        stateMachine.ChangeState(player.dashState);
    }

    private void ChangeToBlackDash()
    {
        stateMachine.ChangeState(player.blackDashState);
    }

    private void ChangeToAttack()
    {
        if (!player.isAttacking)
        {
            stateMachine.ChangeState(player.attackState);
        }
    }

    private void ChangeToFall()
    {
        if (player.rb.linearVelocity.y < -0.1)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}