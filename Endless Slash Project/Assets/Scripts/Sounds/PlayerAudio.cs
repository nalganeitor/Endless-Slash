using UnityEngine;

[CreateAssetMenu(fileName = "Adio Manager", menuName = "Audio Manager/Player Audio")]
public class PlayerAudio : ScriptableObject
{
    [SerializeField] public AudioClip _basicAttackSound;
    [SerializeField] public AudioClip _berserkSound;
    [SerializeField] public AudioClip _flameThrowSound;
    [SerializeField] public AudioClip _explosionSound;
    [SerializeField] public AudioClip _runSound;
}