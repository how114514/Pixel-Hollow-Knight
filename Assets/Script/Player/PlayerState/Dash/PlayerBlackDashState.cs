using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlackDashState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerBlackDashState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.inputControl.GamePlay.Jump.started += JumpInput;

        player.anim.Play("blackDash");
        player.PlaySFX(player.blackDashClip);
        player.canBlackDash = false;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), true);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), true);

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

        player.blackDashCDTimer = 0f;
        player.blackDashCDEnd = false;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), false);

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