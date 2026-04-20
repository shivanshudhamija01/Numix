using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLostPresenter : PresenterBase<GameLostView>
{
    public GameLostPresenter(GameLostView view, IEventBus eventBus) : base(view, eventBus)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnRestart(() => { Debug.Log("Restart the level"); });
        view.OnHome(() =>
        {
            Debug.Log("Home button is clicked");
        });
    }
}
