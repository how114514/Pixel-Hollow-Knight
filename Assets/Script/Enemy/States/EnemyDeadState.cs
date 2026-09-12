using UnityEngine;

public class EnemyDeadState : EnemyState
{
	private enum Phase
	{
		Dying,
		Dead
	}

	private Phase phase;

	private bool reported;

	public EnemyDeadState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.enemy.movement.velocity = Vector2.zero;
		base.enemy.audio.PlayDying();
		CameraShakeManager.Instance?.StartContinuous(0.15f);
		phase = Phase.Dying;
		base.enemy.animation.Play("dying");
	}

	public override void Update()
	{
		switch (phase)
		{
		case Phase.Dying:
			if (base.enemy.animation.IsDone)
			{
				EnterDead();
			}
			break;
		case Phase.Dead:
			if (!reported && base.enemy.animation.IsDone)
			{
				CameraShakeManager.Instance?.StopContinuous();
				reported = true;
				GameEndManager.Instance?.TriggerRestart(10f);
			}
			break;
		}
	}

	private void EnterDead()
	{
		phase = Phase.Dead;
		base.enemy.rb.bodyType = RigidbodyType2D.Static;
		base.enemy.col.enabled = false;
		CameraShakeManager.Instance?.Shake(0.6f);
		base.enemy.audio.PlayDead();
		base.enemy.animation.Play("dead");
	}

	public override void Exit()
	{
		CameraShakeManager.Instance?.StopContinuous();
	}
}
