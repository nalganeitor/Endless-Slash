using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/ShieldBlock")]
public class ShieldBlock : ScriptableObject
{
    public Sprite _shieldBlockIcon;

    public bool _isPlayerBlocking = false;
    public bool _playerStuns = false;

    public float _shieldBlockCooldown = 8f;
    public float _shieldBlockStunDuration = 2f;
}
