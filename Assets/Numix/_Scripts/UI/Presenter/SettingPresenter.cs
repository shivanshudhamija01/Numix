using UnityEngine;

public class SettingPresenter : PresenterBase<SettingView>
{
    public SettingPresenter(SettingView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();

        float bgm = audioService.GetBGMVolume();
        float sfx = audioService.GetSFXVolume();

        view.SetInitialValues(bgm, sfx);
        view.OnExit(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnExitButtonClicked());
        });
        view.OnBGMSliderChanged((value) =>
        {
            audioService.SetBGMVolume(value);
        });

        view.OnSFXSliderChanged((value) =>
        {
            audioService.SetSFXVolume(value);
        });
    }
}
