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
            Debug.Log("Hint Button clicked");
        });
        view.OnPauseButton(() =>
        {
            Debug.Log("Pause Button clicked");
        });
    }
}
