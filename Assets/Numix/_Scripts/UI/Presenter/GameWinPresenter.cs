using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinPresenter : PresenterBase<GameWinView>
{
    private IGameServices gameServices;
    public GameWinPresenter(GameWinView view, IEventBus eventBus) : base(view, eventBus)
    {
        gameServices = ServiceLocator.Get<IGameServices>();
    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnNextLevel(() =>
        {
            Debug.Log("Load Next Level");
            gameServices.CurrentLevel++;
            eventBus.Publish(new Events.OnLoadLevel(gameServices.CurrentLevel));
            eventBus.Publish(new Events.OnNextLevelLoaded());
        });
        view.OnHome(() =>
        {
            Debug.Log("Open the home page");
        });
    }
}
