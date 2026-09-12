public class PlayerActionNoneState : PlayerState
{
	private const float DeadZone = 0.3f;

	public PlayerActionNoneState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.RefreshMovementAnimation();
	}

	public override void Update()
	{
		if (TryEnterPriorityState())
		{
			return;
		}
		if (base.player.input.AttackPressed && base.player.CanAttack)
		{
			base.sm.ChangeState(base.player.attackState);
		}
		else if (base.player.input.SkillPressed && base.player.CanCast)
		{
			float y = base.player.input.MoveInput.y;
			if (y > 0.3f)
			{
				base.sm.ChangeState(base.player.upRoarState);
			}
			else if (y < -0.3f)
			{
				base.sm.ChangeState(base.player.diveState);
			}
			else
			{
				base.sm.ChangeState(base.player.shockwaveState);
			}
		}
		else if (base.player.input.HealHeld && base.player.CanCast && base.player.stats.currentHP < base.player.stats.maxHP)
		{
			base.sm.ChangeState(base.player.healState);
		}
	}
}
