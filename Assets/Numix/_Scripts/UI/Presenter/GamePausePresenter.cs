using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausePresenter : PresenterBase<GamePauseView>
{

    public GamePausePresenter(GamePauseView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
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
        view.OnHome(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnHomeClicked());
        });
        view.OnRestart(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnLevelRestart());
        });
        view.OnSetting(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnSettingButtonClicked());
        });
    }
}
