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
        View.CreateUI();
        View.OnHome(() =>
        {
            Debug.Log("Home button");
        });
        View.OnRestart(() =>
        {
            Debug.Log("Restart the game");
        });
        View.OnSetting(() =>
        {
            Debug.Log("On Setting Button Clicked");
        });
    }
}
