using UnityEngine;

[CreateAssetMenu(fileName = "Adio Manager", menuName = "Audio Manager/Music Theme")]
public class MusicTheme : ScriptableObject
{
    [SerializeField] public AudioClip _musicTheme;
}
