public class EnemyIntroState : EnemyState
{
	private enum Phase
	{
		Fall,
		Roar
	}

	private Phase phase;

	public EnemyIntroState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		phase = Phase.Fall;
		base.enemy.movement.FaceTarget(base.enemy.player.position);
		base.enemy.movement.MoveX(0f);
		base.enemy.animation.Play("fall");
	}

	public override void Update()
	{
		switch (phase)
		{
		case Phase.Fall:
			if (base.enemy.physicsCheck.isGround)
			{
				base.enemy.movement.MoveX(0f);
				phase = Phase.Roar;
				base.enemy.audio.PlayRoar();
				CameraShakeManager.Instance?.StartContinuous(0.2f);
				base.enemy.animation.Play("roar");
			}
			break;
		case Phase.Roar:
			if (base.enemy.animation.IsDone)
			{
				base.sm.ChangeState(base.enemy.decisionState);
			}
			break;
		}
	}

	public override void Exit()
	{
		CameraShakeManager.Instance?.StopContinuous();
	}
}
