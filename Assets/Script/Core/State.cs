public abstract class State<TOwner>
{
	protected StateMachine<TOwner> sm;

	protected TOwner owner;

	public State(StateMachine<TOwner> stateMachine)
	{
		sm = stateMachine;
		owner = stateMachine.owner;
	}

	public virtual void Enter()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void FixedUpdate()
	{
	}

	public virtual void Exit()
	{
	}
}
