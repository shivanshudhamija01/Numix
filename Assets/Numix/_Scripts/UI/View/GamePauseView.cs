using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class GamePauseView : ViewBase
{
    protected override string PackageName => "GamePause";

    protected override string ComponentName => "GamePausePanel";

    private GButton restartBtn;
    private GButton settingBtn;
    private GButton homeBtn;
    private GButton exitBtn;
    protected override void OnCreateUI()
    {
        exitBtn = Panel.GetChild("ExitBtn").asButton;
        restartBtn = Panel.GetChild("RestartBtn").asButton;
        settingBtn = Panel.GetChild("SettingBtn").asButton;
        homeBtn = Panel.GetChild("HomeBtn").asButton;
    }
    public void OnExit(Action action) => exitBtn.onClick.Add(() => action());
    public void OnRestart(Action action) => restartBtn.onClick.Add(() => action());
    public void OnSetting(Action action)
    {
        settingBtn.onClick.Add(() => action());
    }
    public void OnHome(Action action) => homeBtn.onClick.Add(() => action());
}