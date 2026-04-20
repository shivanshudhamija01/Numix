using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinPresenter : PresenterBase<GameWinView>
{
    public GameWinPresenter(GameWinView view, IEventBus eventBus) : base(view, eventBus)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnNextLevel(() =>
        {
            Debug.Log("Load Next Level");
        });
        view.OnHome(() =>
        {
            Debug.Log("Open the home page");
        });
    }
}
