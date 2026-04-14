using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstaller : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private List<SoundData> soundDatas;

    public AudioSource BGMSource => bgmSource;
    public AudioSource SFXSource => sfxSource;
    public List<SoundData> Sounds => soundDatas;
}
