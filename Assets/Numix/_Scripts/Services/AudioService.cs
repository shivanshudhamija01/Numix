using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : IAudioService
{
    private Dictionary<SoundType, SoundData> soundMap = new();
    private AudioSource sfxSource;
    private AudioSource bgmSource;
    public AudioService(AudioInstaller installer)
    {
        sfxSource = installer.SFXSource;
        bgmSource = installer.BGMSource;
        foreach (var sound in installer.Sounds)
        {
            soundMap[sound.id] = sound;
        }
    }

    public void PlaySFX(SoundType type)
    {
        if (soundMap.TryGetValue(type, out var sound))
        {
            sfxSource.PlayOneShot(sound.clip, sound.volume);
        }
    }
    public void PlayBGM(SoundType type)
    {
        if (soundMap.TryGetValue(type, out var sound))
        {
            bgmSource.clip = sound.clip;
            bgmSource.volume = sound.volume;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
}
