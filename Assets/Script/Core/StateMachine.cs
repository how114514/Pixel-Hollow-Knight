public class StateMachine<TOwner>
{
	public State<TOwner> currentState { get; private set; }

	public TOwner owner { get; private set; }

	public StateMachine(TOwner owner)
	{
		this.owner = owner;
	}

	public void Initialize(State<TOwner> startState)
	{
		currentState = startState;
		currentState.Enter();
	}

	public void ChangeState(State<TOwner> newState)
	{
		currentState?.Exit();
		currentState = newState;
		currentState?.Enter();
	}

	public void Update()
	{
		currentState?.Update();
	}

	public void FixedUpdate()
	{
		currentState?.FixedUpdate();
	}
}
