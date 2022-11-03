//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyCombatState : EnemyBaseState
//{
//    IEnumerator ResetAttackCooldown()
//    {
//        yield return new WaitForSeconds(Ctx.AttackCooldown);
//        Ctx.CanAttack = true;
//    }

//    public EnemyCombatState(EnemyStateMachine currentContext, EnemyStateFactory enemyStateFactory)
//    : base(currentContext, enemyStateFactory) 
//    {
//        IsRootState = true;
//    }

//    public override void EnterState()
//    {
//        InitializeSubState();
//    }

//    public override void UpdateState() 
//    {
//        Attack();

//        CheckSwitchStates();
//    }

//    public override void ExitState() { }

//    public override void CheckSwitchStates()
//    {
        
//    }

//    public override void InitializeSubState() 
//    {

//    }

//    public void Attack()
//    {
//        if (Ctx.Enemy != null)
//        {
//            if (Ctx.CanAttack)
//            {
//                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Ctx.AttackPoint.position, Ctx.AttackRange, Ctx.EnemyLayer);

//                foreach (Collider2D knight in hitEnemies)
//                {
//                    knight.GetComponent<PlayerManager>().TakeDamage(Ctx.AttackDamage);
//                }
//                Ctx.Animator.SetTrigger("isAttacking");
//                Ctx.AttackRoutine = Ctx.StartCoroutine(ResetAttackCooldown());
//                Ctx.CanAttack = false;
//            }
//        }
//    }
//}
