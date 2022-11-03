using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowProjectile : MonoBehaviour
{
    [SerializeField] FlameThrow flameThrow;

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            other.GetComponent<EnemyManager>().TakeDamage(flameThrow._flameThrowDamage);
        }
    }
}
