using UnityEngine;

public class EnemyBattleDecisionState : EnemyState
{
	private const float BackJumpChance = 0.2f;

	public EnemyBattleDecisionState(EnemyStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.enemy.animation.Play("idle");
	}

	public override void Update()
	{
		if (base.enemy.animation.IsDone)
		{
			float num = Mathf.Abs(base.enemy.player.position.x - base.enemy.transform.position.x);
			if (Random.value < 0.2f)
			{
				base.sm.ChangeState(base.enemy.backJumpLoopState);
			}
			else if (num >= base.enemy.movement.jumpDistance)
			{
				base.sm.ChangeState(base.enemy.jumpLoopState);
			}
			else
			{
				base.sm.ChangeState(base.enemy.moveLoopState);
			}
		}
	}
}
