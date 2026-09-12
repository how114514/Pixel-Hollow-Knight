public abstract class EnemyState : State<Enemy>
{
	protected new EnemyStateMachine sm => (EnemyStateMachine)base.sm;

	protected Enemy enemy => owner;

	public EnemyState(EnemyStateMachine stateMachine)
		: base((StateMachine<Enemy>)stateMachine)
	{
	}
}
