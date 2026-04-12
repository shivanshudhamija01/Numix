using UnityEngine;
[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData")]
public class SoundData : ScriptableObject
{
    public SoundType id;
    public AudioClip clip;
    public float volume = 1f;
}
