//public abstract class EnemyBaseState
//{
//    private bool _isRootState = false;
//    private EnemyStateMachine _ctx;
//    private EnemyStateFactory _factory;
//    private EnemyBaseState _currentSubState;
//    private EnemyBaseState _currentSuperState;

//    protected bool IsRootState { set { _isRootState = value; } }
//    protected EnemyStateFactory Factory { get { return _factory; } }
//    protected EnemyStateMachine Ctx { get { return _ctx; } }

//    public EnemyBaseState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory) 
//    {
//        _ctx = currentContext;
//        _factory = enemyStateFactory;
//    }

//    public abstract void EnterState();

//    public abstract void UpdateState();

//    public abstract void ExitState();

//    public abstract void CheckSwitchStates();

//    public abstract void InitializeSubState();

//    public void UpdateStates() 
//    {
//        UpdateState();
//        if(_currentSubState != null)
//        {
//            _currentSubState.UpdateStates();
//        }
//    }

//    protected void SwitchState(EnemyBaseState newState) {

//        ExitState();

//        newState.EnterState();

//        if (_isRootState)
//        {
//            _ctx.CurrentState = newState;
//        } 
//        else if (_currentSuperState != null)
//        {
//            _currentSuperState.SetSubState(newState);
//        }
//    }

//    protected void SetSuperState(EnemyBaseState newSuperState) 
//    {
//        _currentSuperState = newSuperState;
//    }

//    protected void SetSubState(EnemyBaseState newSubState) 
//    {
//        _currentSubState = newSubState;
//        newSubState.SetSuperState(this);
//    }
//}
