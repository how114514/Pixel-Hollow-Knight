using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerDashState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.inputControl.GamePlay.Jump.started += JumpInput;

        player.anim.Play("dash");
        player.PlaySFX(player.dashClip);

        player.canDash = false;
        player.isDashing = true;

        player.rb.gravityScale = 0;

        float dir = player.transform.localScale.x;
        player.rb.linearVelocity = new Vector2(dir * player.dashSpeed, 0);
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {
        player.isDashing = false;

        player.inputControl.GamePlay.Jump.started -= JumpInput;
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if (player.physicsCheck.isGround)
        {
            ChangeToJump();
        }

        if ((player.canDoubleJump) && (!player.physicsCheck.isGround))
        {
            ChangeToDoubleJump();
        }
    }

    private void ChangeToDoubleJump()
    {
        stateMachine.ChangeState(player.doubleJumpState);
    }

    private void ChangeToJump()
    {
        stateMachine.ChangeState(player.jumpState);
    }
}