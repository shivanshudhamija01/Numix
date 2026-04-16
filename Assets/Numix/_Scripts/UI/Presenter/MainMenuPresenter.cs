using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPresenter : PresenterBase<MainMenuView>
{
    public MainMenuPresenter(MainMenuView view, EventBus eventBus) : base(view, eventBus)
    {
    }
}
