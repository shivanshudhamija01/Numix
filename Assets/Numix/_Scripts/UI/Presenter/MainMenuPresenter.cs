using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPresenter : PresenterBase<MainMenuView>
{

    public MainMenuPresenter(MainMenuView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnPlay(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnGameStarted());
        });
        view.OnQuit(() =>
        {
            audioService.PlaySFX(SoundType.click);
            Application.Quit();
        });
        view.OnSetting(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnSettingButtonClicked());
        });
    }
}
