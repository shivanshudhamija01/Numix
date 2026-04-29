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
        view.OnExit(() =>
        {
            eventBus.Publish(new Events.OnExitButtonClicked());
        });
        view.OnHome(() =>
        {
            eventBus.Publish(new Events.OnHomeClicked());
        });
        view.OnRestart(() =>
        {
            eventBus.Publish(new Events.OnLevelRestart());
        });
        view.OnSetting(() =>
        {
            Debug.Log("Setting button is clicked");
            eventBus.Publish(new Events.OnSettingButtonClicked());
        });
    }
}
