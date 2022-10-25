using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : EnemyBaseState
{
    public EnemyRunState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
    : base(currentContext, enemyStateFactory) { }

    public override void EnterState() {
     //   _ctx.Rb2D.AddForce(new Vector2(-_ctx.MoveSpeed, 0), ForceMode2D.Impulse);
        Debug.Log("a");
    }

    public override void UpdateState() {
        //_ctx.Rb2D.AddForce(new Vector2(-_ctx.MoveSpeed, 0), ForceMode2D.Impulse);
        Debug.Log("b");

    }

    public override void ExitState() { }

    public override void CheckSwitchStates() {
      /*  if (_ctx.IsAttacking == true)
        {
            SwitchState(_factory.Attack());
        }*/
}

    public override void InitializeState() { }

  /*  void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            bool IsAttacking = true;
        }
    }*/


}
