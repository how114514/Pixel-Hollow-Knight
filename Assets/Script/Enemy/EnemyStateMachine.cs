public class EnemyStateMachine
{
    public EnemyIState currentState;

    public void Initialize(EnemyIState startState)
    {
        currentState = startState;
        currentState.OnEnter();
    }

    public void ChangeState(EnemyIState newState)
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
