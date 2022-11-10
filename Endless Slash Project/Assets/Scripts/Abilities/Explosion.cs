using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/Explosion")]
public class Explosion : ScriptableObject
{
    public GameObject _explosion;
    public Sprite _explosionIcon;

    public bool _canExplosion = true;

    public float _explosionCooldown = 7f;
    public float _explosionDamage = 1000f;
    public float _explosionDuration = 2f;
    public float _explosionXPos = -1.43f;
    public float _explosionYPos = 17.29269f;
}
