using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPresenter : PresenterBase<MainMenuView>
{

    public MainMenuPresenter(MainMenuView view, IEventBus eventBus) : base(view, eventBus)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnPlay(() =>
        {
            eventBus.Publish(new Events.OnGameStarted());
            Debug.Log("Play button is clicked");
        });
        view.OnQuit(() =>
        {
            Debug.Log("Quit Game");
        });
        view.OnSetting(() =>
        {
            eventBus.Publish(new Events.OnSettingButtonClicked());
            Debug.Log("Setting button is clicked");
        });
    }
}
