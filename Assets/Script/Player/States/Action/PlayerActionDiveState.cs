using UnityEngine;

public class PlayerActionDiveState : PlayerState
{
	private enum Phase
	{
		Windup,
		Diving,
		Recover
	}

	private Phase phase;

	public PlayerActionDiveState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.movementSM.ChangeState(base.player.idleState);
		base.player.movementLocked = true;
		if (!base.player.stats.TryConsumeEnergy(3))
		{
			base.sm.ChangeState(base.player.noneState);
		}
		else
		{
			EnterWindup();
		}
	}

	public override void Update()
	{
		if (!TryEnterPriorityState())
		{
			switch (phase)
			{
			case Phase.Windup:
				TickWindup();
				break;
			case Phase.Diving:
				TickDiving();
				break;
			case Phase.Recover:
				TickRecover();
				break;
			}
		}
	}

	public override void Exit()
	{
		base.player.movementLocked = false;
		base.player.movement.gravityScale = 2f;
		RestoreEnemyCollision();
	}

	private void EnterWindup()
	{
		phase = Phase.Windup;
		base.player.movement.velocity = new Vector2(0f, base.player.ability.diveRiseForce);
		base.player.animation.Play("diveWindup");
	}

	private void TickWindup()
	{
		if (base.player.rb.linearVelocity.y <= 0f)
		{
			EnterDiving();
		}
	}

	private void EnterDiving()
	{
		phase = Phase.Diving;
		IgnoreEnemyCollision();
		base.player.movement.velocity = new Vector2(0f, 0f - base.player.ability.diveForce);
		base.player.audio.PlayDive();
		base.player.animation.Play("diving");
	}

	private void TickDiving()
	{
		base.player.movement.velocity = new Vector2(0f, 0f - base.player.ability.diveForce);
		if (base.player.physicsCheck.isGround)
		{
			EnterRecover();
		}
	}

	private void EnterRecover()
	{
		phase = Phase.Recover;
		base.player.movement.velocity = Vector2.zero;
		base.player.ability.SpawnGroundwave();
		base.player.canDashFlag = true;
		base.player.canDoubleJump = true;
		base.player.audio.PlayDiveLand();
		CameraShakeManager.Instance?.Shake(0.5f);
		base.player.animation.Play("diveRecover");
	}

	private void TickRecover()
	{
		if (base.player.animation.IsDone)
		{
			base.sm.ChangeState(base.player.noneState);
		}
	}

	private void IgnoreEnemyCollision()
	{
		SetEnemyCollision(ignore: true);
	}

	private void RestoreEnemyCollision()
	{
		SetEnemyCollision(ignore: false);
	}

	private static void SetEnemyCollision(bool ignore)
	{
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyBody"), ignore);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemyAttack"), ignore);
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBody"), LayerMask.NameToLayer("EnemySkill"), ignore);
	}
}
