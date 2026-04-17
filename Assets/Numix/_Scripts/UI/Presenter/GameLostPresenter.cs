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
        View.CreateUI();
        View.OnRestart(() => { Debug.Log("Restart the level"); });
        View.OnHome(() =>
        {
            Debug.Log("Home button is clicked");
        });
    }
}
