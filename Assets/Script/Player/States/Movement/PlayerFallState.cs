using UnityEngine;

public class PlayerFallState : PlayerState
{
	private float inputX;

	public PlayerFallState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		if (!base.player.IsActing)
		{
			base.player.animation.Play("fall");
		}
	}

	public override void Update()
	{
		if (base.player.movementLocked)
		{
			return;
		}
		inputX = base.player.input.MoveInput.x;
		if (!TryDash())
		{
			if (Mathf.Abs(inputX) > 0.01f)
			{
				base.player.transform.localScale = new Vector3((inputX > 0f) ? 1 : (-1), 1f, 1f);
			}
			if (base.player.input.JumpPressed && base.player.canDoubleJump)
			{
				base.player.jumpState.isDoubleJump = true;
				base.sm.ChangeState(base.player.jumpState);
			}
		}
	}

	public override void FixedUpdate()
	{
		if (!base.player.movementLocked)
		{
			if (!base.player.isRecoiling)
			{
				base.player.movement.MoveX(inputX * base.player.movement.moveSpeed);
			}
			if (base.player.physicsCheck.isGround)
			{
				base.sm.ChangeState(base.player.idleState);
			}
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
