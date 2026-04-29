using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPresenter : PresenterBase<GamePlayView>
{
    public GamePlayPresenter(GamePlayView view, IEventBus eventBus) : base(view, eventBus)
    {
    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnHintButton(() =>
        {
            eventBus.Publish(new Events.OnHintRequested());
        });
        view.OnPauseButton(() =>
        {
            eventBus.Publish(new Events.OnGamePaused());
        });
    }
}
