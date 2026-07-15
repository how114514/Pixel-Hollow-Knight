public class PlayerStateMachine
{
    public PlayerIState currentState;

    public void Initialize(PlayerIState startState)
    {
        currentState = startState;
        currentState.OnEnter();
    }

    public void ChangeState(PlayerIState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    public void Update()
    {
        currentState.OnUpdate();
    }
}
