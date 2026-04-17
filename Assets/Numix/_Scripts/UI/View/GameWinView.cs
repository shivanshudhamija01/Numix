using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class GameWinView : ViewBase
{
    protected override string PackageName => "GameWin";

    protected override string ComponentName => "GameWinPanel";

    private GButton nxtLevel;
    private GButton home;
    protected override void OnCreateUI()
    {
        nxtLevel = Panel.GetChild("NxtLevel").asButton;
        home = Panel.GetChild("Home").asButton;
    }

    public void OnNextLevel(Action action) => nxtLevel.onClick.Add(() => action());
    public void OnHome(Action action) => home.onClick.Add(() => action());
}
