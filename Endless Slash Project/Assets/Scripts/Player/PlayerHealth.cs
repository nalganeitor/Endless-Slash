using UnityEngine;

[CreateAssetMenu(fileName = "PlayerManager", menuName = "PlayerManager/Health")]
public class PlayerHealth : ScriptableObject
{
    public float _playerCurrentHealth;
    public float _playerMaxHealth = 200000f;
}
