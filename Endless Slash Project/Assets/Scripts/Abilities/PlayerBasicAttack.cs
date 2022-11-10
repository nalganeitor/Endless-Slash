using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/BasicAttack")]
public class PlayerBasicAttack : ScriptableObject
{
    public Sprite _playerBasicAttackIcon;

    public bool _canPlayerBasicAttack = true;

    public float _playerBasicAttackCooldown = 1f;
    public float _playerBasicAttackDamage = 400f;
}
