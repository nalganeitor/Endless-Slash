using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAbilities", menuName = "PlayerAbilities/Berserk")]
public class Berserk : ScriptableObject
{
    public Sprite _berserkIcon;

    public bool _berserkMode = false;
    public bool _canBerserk = false;

    public float _berserkCooldown = 8f;
    public float _berserkDuration = 4f;
}
