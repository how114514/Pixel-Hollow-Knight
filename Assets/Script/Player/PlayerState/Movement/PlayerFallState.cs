using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallState : PlayerIState
{
    PlayerController player;
    PlayerStateMachine stateMachine;

    public PlayerFallState(PlayerController player, PlayerStateMachine machine)
    {
        this.player = player;
        stateMachine = machine;
    }

    public void OnEnter()
    {
        player.inputControl.GamePlay.Dash.started += DashInput;
        player.inputControl.GamePlay.Jump.started += JumpInput;
        player.inputControl.GamePlay.Attack.started += AttackInput;
        player.inputControl.GamePlay.Skill.started += SkillInput;

        player.anim.Play("fall");
    }

    public void OnUpdate()
    {
        Move();

        if (player.physicsCheck.isGround)
        {
            ChangeToIdle();
        }
    }

    public void OnExit()
    {
        player.inputControl.GamePlay.Dash.started -= DashInput;
        player.inputControl.GamePlay.Jump.started -= JumpInput;
        player.inputControl.GamePlay.Attack.started -= AttackInput;
        player.inputControl.GamePlay.Skill.started -= SkillInput;
    }

    private void Move()
    {
        Vector2 inputDirection = player.inputControl.GamePlay.Move.ReadValue<Vector2>();

        player.rb.linearVelocity = new Vector2(inputDirection.x * player.moveSpeed, player.rb.linearVelocity.y);

        int faceDir = (int)player.transform.localScale.x;

        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        if (inputDirection.x < 0)
        {
            faceDir = -1;
        }

        player.transform.localScale = new Vector3(faceDir, 1, 1);
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

    private void AttackInput(InputAction.CallbackContext context)
    {
        ChangeToAttack();
    }

    private void JumpInput(InputAction.CallbackContext context)
    {
        if (player.canDoubleJump)
        {
            ChangeToDoubleJump();
        }
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

    private void ChangeToIdle()
    {
        stateMachine.ChangeState(player.idleState);
    }

    private void ChangeToDash()
    {
        stateMachine.ChangeState(player.dashState);
    }

    private void ChangeToBlackDash()
    {
        stateMachine.ChangeState(player.blackDashState);
    }

    private void ChangeToDoubleJump()
    {
        stateMachine.ChangeState(player.doubleJumpState);
    }

    private void ChangeToAttack()
    {
        stateMachine.ChangeState(player.attackState);
    }
}