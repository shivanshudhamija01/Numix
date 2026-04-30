using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPresenter : PresenterBase<GamePlayView>
{
    public GamePlayPresenter(GamePlayView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {
    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnHintButton(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnHintRequested());
        });
        view.OnPauseButton(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnGamePaused());
        });
    }
}
