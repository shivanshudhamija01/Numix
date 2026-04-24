using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorPresenter : PresenterBase<LevelSelectorView>
{
    public LevelSelectorPresenter(LevelSelectorView view, IEventBus eventBus) : base(view, eventBus)
    {
    }
    public override void Initialize()
    {
        view.SetLevel(50);
        view.CreateUI();
    }
}
