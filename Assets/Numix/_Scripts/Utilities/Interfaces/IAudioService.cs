public interface IAudioService
{
    void PlayBGM(SoundType soundType);
    void PlaySFX(SoundType soundType);
    void SetBGMVolume(float value);
    void SetSFXVolume(float value);
    float GetBGMVolume();
    float GetSFXVolume();
}
