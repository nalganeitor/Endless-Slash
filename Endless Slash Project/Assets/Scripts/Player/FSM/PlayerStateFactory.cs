//using System.Collections.Generic;

//enum PlayerStates
//{
//    Combat,
//    Run
//}

//public class PlayerStateFactory
//{
//    PlayerStateMachine _context;

//    Dictionary<PlayerStates, PlayerBaseState> _states = new Dictionary<PlayerStates, PlayerBaseState>();

//    public PlayerStateFactory(PlayerStateMachine currentContext)
//    {
//        _context = currentContext;
//        _states[PlayerStates.Combat] = new PlayerCombatState(_context, this);
//        _states[PlayerStates.Run] = new PlayerRunState(_context, this);
//    }

//    public PlayerBaseState Combat()
//    {
//        return _states[PlayerStates.Combat];
//    }
//    public PlayerBaseState Run()
//    {
//        return _states[PlayerStates.Run];
//    }
//}
