using UnityEngine;

public enum Stats
{
    Damage,
    Defense,
    Health
}

[CreateAssetMenu(fileName = "PlayerStats", menuName = "PlayerStats/Warrior")]
public class PlayerStats : ScriptableObject
{
    public int damageStat = 81;
    public int defenseStat = 1;
    public int healthStat = 1;
}