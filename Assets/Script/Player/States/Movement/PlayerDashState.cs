using UnityEngine;

public class PlayerDashState : PlayerState
{
	private bool isBlackDash;

	public PlayerDashState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		isBlackDash = base.player.canBlackDash;
		base.player.canDashFlag = false;
		if (isBlackDash)
		{
			base.player.canBlackDash = false;
			IgnoreEnemyCollision(ignore: true);
		}
		if (!base.player.IsActing)
		{
			if (isBlackDash)
			{
				base.player.audio.PlayBlackDash();
			}
			else
			{
				base.player.audio.PlayDash();
			}
			base.player.animation.Play(isBlackDash ? "blackDash" : "dash");
		}
		base.player.movement.gravityScale = 0f;
		base.player.movement.gravityLocked = true;
		base.player.movement.velocity = Vector2.zero;
		float x = base.player.transform.localScale.x;
		base.player.movement.MoveX(x * base.player.movement.dashSpeed);
	}

	public override void Update()
	{
		if (!base.player.animation.IsDone)
		{
			return;
		}
		base.sm.ChangeState(base.player.fallState);
		if (base.player.input.JumpPressed)
		{
			if (base.player.physicsCheck.isGround)
			{
				base.player.jumpState.isDoubleJump = false;
				base.sm.ChangeState(base.player.jumpState);
			}
			else if (base.player.canDoubleJump)
			{
				base.player.jumpState.isDoubleJump = true;
				base.sm.ChangeState(base.player.jumpState);
			}
		}
	}

	private void IgnoreEnemyCollision(bool ignore)
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), ignore);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), ignore);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), ignore);
	}

	public override void Exit()
	{
		base.player.movement.gravityLocked = false;
		base.player.movement.gravityScale = 2f;
		if (isBlackDash)
		{
			IgnoreEnemyCollision(ignore: false);
			base.player.blackDashTimer = 0f;
			base.player.blackDashCharging = true;
		}
	}
}
