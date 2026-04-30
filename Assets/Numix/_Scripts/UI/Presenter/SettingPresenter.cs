using UnityEngine;

public class SettingPresenter : PresenterBase<SettingView>
{
    public SettingPresenter(SettingView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnExit(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnExitButtonClicked());
        });
    }
}
