public class PlayerActionStunnedState : PlayerState
{
	public PlayerActionStunnedState(PlayerStateMachine sm)
		: base(sm)
	{
	}

	public override void Enter()
	{
		base.player.movementSM.ChangeState(base.player.idleState);
		base.player.movementLocked = true;
		base.player.animation.Play("stunned");
	}

	public override void Update()
	{
		if (!base.player.stunnedActive)
		{
			base.sm.ChangeState(base.player.noneState);
		}
		else
		{
			TryEnterPriorityState();
		}
	}

	public override void Exit()
	{
		base.player.movementLocked = false;
	}
}
