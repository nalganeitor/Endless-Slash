using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "EnemyManager/EnemyHealth")]
public class EnemyHealth : ScriptableObject
{
    public float _enemyCurrentHealth;
    public float _enemyMaxHealth = 200000f;
}