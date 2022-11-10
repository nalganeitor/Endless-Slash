using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowInstantiate : MonoBehaviour
{
    [SerializeField] FlameThrow flameThrow;

    void Update()
    {
        transform.Translate(Vector2.right * flameThrow._flameThrowspeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyManager>().TakeDamage(flameThrow._flameThrowDamage);
        }
    }
}
