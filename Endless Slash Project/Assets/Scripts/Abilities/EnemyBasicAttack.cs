using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAbilities", menuName = "EnemyAbilities/EnemyBasicAttack")]
public class EnemyBasicAttack : ScriptableObject
{
    [SerializeField] GameObject _enemyDemon;

    public Sprite _enemyBasicAttackIcon;

    public bool _canEnemyBasicAttack = false;

    public float _enemyBasicAttackCooldown = 5f;
    public float _enemyBasicAttackDamage = 40;
}