//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyIdleState : EnemyBaseState
//{
//    public EnemyIdleState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
//    : base (currentContext, enemyStateFactory) 
//    {
//        IsRootState = true;
//    }

//    public override void EnterState() 
//    {
//        InitializeSubState();
//    }

//    public override void UpdateState() 
//    {
//        CheckSwitchStates();
//    }

//    public override void ExitState() { }

//    public override void CheckSwitchStates()
//    {
//        if (Ctx.IsEnemyNear)
//        {
//            SwitchState(Factory.Combat());
//        }
//    }

//    public override void InitializeSubState() { }
//}
