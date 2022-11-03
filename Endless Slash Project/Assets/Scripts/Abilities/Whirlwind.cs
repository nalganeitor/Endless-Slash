using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/Whirlwind")]
public class Whirlwind : ScriptableObject
{
    public Sprite whirlwindIcon;

    public bool _canWhirlwind = true;

    public float _whirlwindAmountDamaged = 0;
    public float _whirlwindAttackRange = 1f;
    public float _whirlwindCooldown = 10f;
    public float _whirlwindDamageAmount = 2000;
    public float _whirlwindDamagePerLoop = 200f;
    //  public float _wirlwindDamageTime = 10f;
    public float _whirlwindDuration = 10f;
}

