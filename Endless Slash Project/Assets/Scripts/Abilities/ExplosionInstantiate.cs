using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionInstantiate : MonoBehaviour
{
    [SerializeField] Explosion explosion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
           other.GetComponent<EnemyManager>().TakeDamage(explosion._explosionDamage);
        }
    }
}
