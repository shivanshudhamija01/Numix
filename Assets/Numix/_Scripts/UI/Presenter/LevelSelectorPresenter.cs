using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorPresenter : PresenterBase<LevelSelectorView>
{
    public LevelSelectorPresenter(LevelSelectorView view, IEventBus eventBus, IAudioService audioService) : base(view, eventBus, audioService)
    {

    }
    public override void Initialize()
    {
        view.SetLevel(50);
        view.CreateUI();
        view.OnLevelClicked += HandleLevelClicked;
    }

    private void HandleLevelClicked(int levelIndex)
    {
        audioService.PlaySFX(SoundType.click);
        eventBus.Publish(new Events.OnLoadLevel(levelIndex));
    }
    public override void Dispose()
    {
        view.OnLevelClicked -= HandleLevelClicked;
    }
}
