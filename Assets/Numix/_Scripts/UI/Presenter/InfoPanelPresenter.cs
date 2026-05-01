using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelPresenter : PresenterBase<InfoPanelView>
{
    public InfoPanelPresenter(InfoPanelView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {
    }

    public override void Initialize()
    {
        view.CreateUI();

        view.OnExit(() =>
        {
            eventBus.Publish(new Events.OnExitButtonClicked());
        });
    }
}
