public class PlayerStateMachine : StateMachine<Player>
{
	public Player player => base.owner;

	public PlayerStateMachine(Player player)
		: base(player)
	{
	}
}
