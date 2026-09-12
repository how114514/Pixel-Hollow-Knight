using UnityEngine;

public class PlayerJumpState : PlayerState
{
	private float inputX;

	public bool isDoubleJump;

	public PlayerJumpState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		if (isDoubleJump)
		{
			base.player.canDoubleJump = false;
			base.player.audio.PlayDoubleJump();
			base.player.movement.velocity = new Vector2(base.player.rb.linearVelocity.x, base.player.movement.doubleJumpForce);
		}
		else
		{
			base.player.movement.AddForce(Vector2.up * base.player.movement.jumpForce);
		}
		if (!base.player.IsActing)
		{
			base.player.animation.Play(isDoubleJump ? "doubleJump" : "jump");
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
			if (!isDoubleJump && !base.player.input.JumpHeld && base.player.rb.linearVelocity.y > 0f)
			{
				base.player.movement.velocity = new Vector2(base.player.rb.linearVelocity.x, 0f);
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
			if (base.player.rb.linearVelocity.y < -0.1f)
			{
				base.sm.ChangeState(base.player.fallState);
			}
			else if (base.player.physicsCheck.isGround && base.player.rb.linearVelocity.y <= 0.01f)
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
