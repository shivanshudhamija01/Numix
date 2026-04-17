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
        View.CreateUI();
        View.OnNextLevel(() =>
        {
            Debug.Log("Load Next Level");
        });
        View.OnHome(() =>
        {
            Debug.Log("Open the home page");
        });
    }
}
