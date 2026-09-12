public abstract class PlayerState : State<Player>
{
	protected new PlayerStateMachine sm => (PlayerStateMachine)base.sm;

	protected Player player => owner;

	public PlayerState(PlayerStateMachine stateMachine)
		: base((StateMachine<Player>)stateMachine)
	{
	}

	protected bool TryEnterPriorityState()
	{
		if (player.pendingDead)
		{
			player.pendingDead = false;
			sm.ChangeState(player.deadState);
			return true;
		}
		if (player.pendingHurt)
		{
			player.pendingHurt = false;
			sm.ChangeState(player.hurtState);
			return true;
		}
		if (player.stunnedActive && sm.currentState != player.stunnedState)
		{
			sm.ChangeState(player.stunnedState);
			return true;
		}
		return false;
	}
}
