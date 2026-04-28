using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class GamePlayView : ViewBase
{
    protected override string PackageName => "GamePlay";

    protected override string ComponentName => "GamePlayPanel";
    private GButton pauseButton;
    private GButton hintButton;

    protected override void OnCreateUI()
    {
        pauseButton = Panel.GetChild("PauseBtn").asButton;
        hintButton = Panel.GetChild("HintBtn").asButton;
    }
    public void OnPauseButton(Action action) => pauseButton.onClick.Add(() => action());
    public void OnHintButton(Action action) => hintButton.onClick.Add(() => action());
}
