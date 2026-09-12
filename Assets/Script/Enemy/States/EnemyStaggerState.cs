using UnityEngine;

public class EnemyStaggerState : EnemyState
{
	private float timer;

	private const float StaggerDuration = 3f;

	private const float KnockbackForce = 5f;

	public EnemyStaggerState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.enemy.pendingStaggerExit = false;
		timer = 3f;
		base.enemy.movement.MoveX(0f);
		Vector2 vector = ((Vector2)base.enemy.transform.position - base.enemy.lastDamageSourcePos).normalized;
		if (vector.sqrMagnitude < 0.01f)
		{
			vector = Vector2.right;
		}
		base.enemy.movement.velocity = vector * 5f;
		base.enemy.movement.FaceTarget(base.enemy.player.position);
		base.enemy.audio.PlayStagger();
		CameraShakeManager.Instance?.Shake(0.3f);
		base.enemy.animation.Play("stagger");
	}

	public override void Update()
	{
		if (base.enemy.pendingStaggerExit)
		{
			base.sm.ChangeState(base.enemy.decisionState);
			return;
		}
		timer -= Time.deltaTime;
		if (timer <= 0f)
		{
			base.sm.ChangeState(base.enemy.decisionState);
		}
	}

	public override void Exit()
	{
	}
}
