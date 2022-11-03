using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/FlameThrow")]
public class FlameThrow : ScriptableObject
{
    public GameObject _flameThrow;

    public Sprite flameThrowIcon;

    public bool _canFlameThrow = true;

    public float _flameThrowCooldown = 6f;
    public float _flameThrowDamage = 200f;
    public float _flameThrowDuration = 5f;
    public float _flameThrowXPos = -9.78f;
    public float _flameThrowYPos = 14.16f;
}  
