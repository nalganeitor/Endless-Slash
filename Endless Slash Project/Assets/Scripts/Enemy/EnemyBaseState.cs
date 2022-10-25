public abstract class EnemyBaseState
{
    protected EnemyStateMachine _ctx;
    protected EnemyStateFactory _factory;
    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory) {
        _ctx = currentContext;
        _factory = enemyStateFactory;
    }


    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeState();

    void UpdateStates() { }

    protected void SwitchState(EnemyBaseState newState) {

        ExitState();

        newState.EnterState();

        _ctx.CurrentState = newState;
    }

    protected void SetSuperState() { }

    protected void SetSubState() { }
}
