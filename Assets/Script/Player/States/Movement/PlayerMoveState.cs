using UnityEngine;

public class PlayerMoveState : PlayerState
{
	private float inputX;

	public PlayerMoveState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		if (!base.player.IsActing)
		{
			base.player.animation.Play("move");
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
			if (Mathf.Abs(inputX) < 0.01f)
			{
				base.sm.ChangeState(base.player.idleState);
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
		if (!base.player.movementLocked)
		{
			if (!base.player.isRecoiling)
			{
				base.player.movement.MoveX(inputX * base.player.movement.moveSpeed);
			}
			if (!base.player.physicsCheck.isGround)
			{
				base.sm.ChangeState(base.player.fallState);
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
