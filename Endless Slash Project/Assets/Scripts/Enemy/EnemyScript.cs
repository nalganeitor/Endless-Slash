using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Animator animator;

    [SerializeField] GameObject player;

    Rigidbody2D rb2D;

    int isAttackingHash;

    public Transform attackPoint;

    public float attackRange;

    public LayerMask enemyLayer;

    public int attackDamage = 40;

    public bool canAttack = false;

    float attackCooldown = 5f;

    int attackRangeHash;

    bool enemyNear = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        isAttackingHash = Animator.StringToHash("isAttacking");
        attackRangeHash = Animator.StringToHash("attackRange");

        rb2D = GetComponent<Rigidbody2D>();

        EnemyRun();
    }

    void Update()
    {
        Attack();

        if (enemyNear == false)
        {
            EnemyRun();
        }
    }

    void EnemyRun()
    {
        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance > 0.05f && distance < 50f)
        {
            transform.position -= transform.right * (Time.deltaTime * 3);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("a");

            animator.SetBool(attackRangeHash, true);

            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;

            enemyNear = true;

            canAttack = true;
        }
    }

    public void Attack()
    {
        if (canAttack == true)
        {
            animator.SetTrigger("isAttacking");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D knight in hitEnemies)
            {
                knight.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }

            animator.SetTrigger("isAttacking");
            StartCoroutine(ResetAttackCooldown());
            canAttack = false;

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
