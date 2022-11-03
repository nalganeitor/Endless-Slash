//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCombatState : PlayerBaseState
//{
//    IEnumerator ResetAttackCooldown()
//    {
//        yield return new WaitForSeconds(Ctx.AttackCooldown);
//        Ctx.CanAttack = true;
//    }

//    public PlayerCombatState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
//    : base(currentContext, playerStateFactory)
//    {
//        IsRootState = true;
//    }

//    public override void EnterState()
//    {
//        Ctx.Animator.SetBool(Ctx.IsCombatingHash, true);

//        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);

//        InitializeSubState();
//    }

//    public override void UpdateState()
//    {
//      //  Attack();

//        CheckSwitchStates();
//    }

//    public override void ExitState() { }

//    public override void CheckSwitchStates()
//    {
//        if (!Ctx.IsEnemyNear && Ctx.KeepRunning)
//        {
//            SwitchState(Factory.Run());
//        }
//    }

//    public override void InitializeSubState()
//    {

//    }

//  /*  public void Attack()
//    {
//        if (Ctx.IsAttackPressed && Ctx.CanAttack)
//        {
//            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Ctx.AttackPoint.position, Ctx.AttackRange, Ctx.EnemyLayer);

//            foreach (Collider2D Demons in hitEnemies)
//            {
//                Demons.GetComponent<EnemyStateMachine>().TakeDamage(Ctx.AttackDamage);
//            }

//            Ctx.Source.GenerateImpulse();
//            Ctx.Animator.SetTrigger("Attack");
//            Ctx.StartAttackRoutine = Ctx.StartCoroutine(ResetAttackCooldown());
//            Ctx.CanAttack = false;
//        }
//    }*/
//}
