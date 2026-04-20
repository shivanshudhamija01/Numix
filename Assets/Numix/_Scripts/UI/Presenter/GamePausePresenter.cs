using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausePresenter : PresenterBase<GamePauseView>
{
    public GamePausePresenter(GamePauseView view, IEventBus eventBus) : base(view, eventBus)
    {
    }

    public override void Initialize()
    {
        view.CreateUI();
        view.OnHome(() =>
        {
            Debug.Log("Home button");
        });
        view.OnRestart(() =>
        {
            Debug.Log("Restart the game");
        });
        view.OnSetting(() =>
        {
            Debug.Log("On Setting Button Clicked");
        });
    }
}
