public class EnemyStateFactory
{
    EnemyStateMachine _context;

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
    }

    public EnemyBaseState Attack() {
        return new EnemyAttackState(_context, this);
    }
    public EnemyBaseState Death() {
        return new EnemyDeathState(_context, this);
    }
    public EnemyBaseState Idle() {
        return new EnemyIdleState(_context, this);
    }
    public EnemyBaseState Run() {
        return new EnemyRunState(_context, this);
    }
}
