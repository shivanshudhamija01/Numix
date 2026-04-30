using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinPresenter : PresenterBase<GameWinView>
{
    private IGameServices gameServices;
    public GameWinPresenter(GameWinView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {
        gameServices = ServiceLocator.Get<IGameServices>();
    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnNextLevel(() =>
        {
            audioService.PlaySFX(SoundType.click);
            gameServices.CurrentLevel++;
            eventBus.Publish(new Events.OnLoadLevel(gameServices.CurrentLevel));
            eventBus.Publish(new Events.OnNextLevelLoaded());
        });
        view.OnHome(() =>
        {
            audioService.PlaySFX(SoundType.click);
            eventBus.Publish(new Events.OnHomeClicked());
        });
    }
}
