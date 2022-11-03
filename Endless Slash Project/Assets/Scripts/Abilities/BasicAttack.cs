using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/BasicAttack")]
public class BasicAttack : ScriptableObject

{
    public Sprite _basicAttackIcon;

    public bool _canBasicAttack = true;

    public float _basicAttackCooldown = 1f;
    public float _basicAttackDamage = 400f;
}
