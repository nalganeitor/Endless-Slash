using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public InventoryObject inventory;

    Animator animator;

    Rigidbody2D rb2D;

    public Transform attackPoint;

    public float attackRange;

    public LayerMask enemyLayer;

    int isAttackingHash;

    public int attackDamage = 40;

    float attackCooldown = 1f;

    bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();

        rb2D = GetComponent<Rigidbody2D>();

        rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;

        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            animator.SetTrigger("Attack");

            Attack();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            inventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            inventory.Load();
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D Demon in hitEnemies)
        {
            Demon.GetComponent<HealthScript>().TakeDamage(attackDamage);
        }

        StartCoroutine(ResetAttackCooldown());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();

        if (/*other.tag == "Enemy" || */item)
        {
            //Debug.Log("caca");
          //  animator.SetBool(isAttackingHash, true);

            inventory.AddItem(item.item, 0.5f);
            Destroy(other.gameObject);
            Debug.Log("a");
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

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
