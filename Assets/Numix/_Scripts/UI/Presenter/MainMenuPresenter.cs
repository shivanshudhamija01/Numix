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
        View.CreateUI();
        View.OnPlay(() =>
        {
            Debug.Log("Play button is clicked");
        });
        View.OnQuit(() =>
        {
            Debug.Log("Quit Game");
        });
        View.OnSetting(() =>
        {
            Debug.Log("Setting button is clicked");
        });
    }
}
