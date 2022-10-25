using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    int maxHealth = 200;
    public int currentHealth;
    int isHurtHash;

  //  [SerializeField] Transform transform;

    [SerializeField] GameObject chestModel;

    int isDeadHash;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        isHurtHash = Animator.StringToHash("isHurt");

        isDeadHash = Animator.StringToHash("isDead");

        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("isHurt");

        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool(isDeadHash, true);

        DropChest();

        StartCoroutine(SelfDestruct());
    }

    void DropChest()
    {
        Vector3 position = transform.position;
        GameObject chest = Instantiate(chestModel, position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
        chest.SetActive(true);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
