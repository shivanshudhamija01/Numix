using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLostPresenter : PresenterBase<GameLostView>
{
    public GameLostPresenter(GameLostView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnRestart(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnLevelRestart());
        });
        view.OnHome(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnHomeClicked());
        });
    }
}
