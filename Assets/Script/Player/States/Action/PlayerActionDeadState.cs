using UnityEngine;

public class PlayerActionDeadState : PlayerState
{
	private enum Phase
	{
		Dying,
		Dead
	}

	private Phase phase;

	private bool reported;

	public PlayerActionDeadState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.movementSM.ChangeState(base.player.idleState);
		base.player.movementLocked = true;
		base.player.movement.gravityScale = 0f;
		base.player.movement.velocity = Vector2.zero;
		EnterDying();
	}

	public override void Update()
	{
		switch (phase)
		{
		case Phase.Dying:
			TickDying();
			break;
		case Phase.Dead:
			TickDead();
			break;
		}
	}

	private void EnterDying()
	{
		phase = Phase.Dying;
		CameraShakeManager.Instance?.StartContinuous(0.15f);
		base.player.col.enabled = false;
		base.player.rb.bodyType = RigidbodyType2D.Static;
		base.player.animation.Play("dying");
	}

	private void TickDying()
	{
		if (base.player.animation.IsDone)
		{
			EnterDead();
		}
	}

	private void EnterDead()
	{
		phase = Phase.Dead;
		base.player.animation.Play("dead");
	}

	private void TickDead()
	{
		if (!reported)
		{
			reported = true;
			GameEndManager.Instance?.TriggerRestart(3f);
		}
	}

	public override void Exit()
	{
		CameraShakeManager.Instance?.StopContinuous();
	}
}
