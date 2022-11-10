using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiate : MonoBehaviour
{
    [SerializeField] GameObject prefabPool;
    float xpos = 4.29f;
    float ypos = 15.14f;
    float enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10)
        {
            var copy = prefabPool;

            Instantiate(copy, new Vector3(xpos, ypos, 0), Quaternion.identity);
            yield return new WaitForSeconds(2);
            enemyCount += 1;
        }
    }
}

