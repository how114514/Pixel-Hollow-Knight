using UnityEngine;

public class PlayerIdleState : PlayerState
{
	public PlayerIdleState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.canDashFlag = true;
		base.player.canDoubleJump = true;
		base.player.movement.MoveX(0f);
		if (!base.player.IsActing)
		{
			base.player.animation.Play("idle");
		}
	}

	public override void Update()
	{
		if (!base.player.movementLocked && !TryDash())
		{
			if (Mathf.Abs(base.player.input.MoveInput.x) > 0.01f)
			{
				base.sm.ChangeState(base.player.moveState);
			}
			else if (base.player.input.JumpPressed && base.player.physicsCheck.isGround)
			{
				base.player.jumpState.isDoubleJump = false;
				base.sm.ChangeState(base.player.jumpState);
			}
		}
	}

	public override void FixedUpdate()
	{
		if (!base.player.movementLocked && !base.player.physicsCheck.isGround)
		{
			base.sm.ChangeState(base.player.fallState);
		}
	}

	private bool TryDash()
	{
		if (base.player.input.DashPressed && base.player.CanDash)
		{
			base.sm.ChangeState(base.player.dashState);
			return true;
		}
		return false;
	}
}
