public class EnemyStateMachine : StateMachine<Enemy>
{
    public Enemy enemy => owner;

    public EnemyStateMachine(Enemy enemy) : base(enemy) { }
}
