using System.Collections.Generic;

enum EnemyStates
{
    Combat,
    Idle
}

public class EnemyStateFactory
{
    EnemyStateMachine _context;

    Dictionary<EnemyStates, EnemyBaseState> _states = new Dictionary<EnemyStates, EnemyBaseState>();
    
    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
        _states[EnemyStates.Combat] = new EnemyCombatState(_context, this);
        _states[EnemyStates.Idle] = new EnemyIdleState(_context, this);
    }

    public EnemyBaseState Combat() 
    {
        return _states[EnemyStates.Combat];
    }
    public EnemyBaseState Run() 
    {
        return _states[EnemyStates.Idle];
    }
}
