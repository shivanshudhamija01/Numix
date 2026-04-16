using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class MainMenuView : ViewBase
{
    protected override string PackageName => "MainMenuView";
    protected override string ComponentName => "MainMenuPanel";
    private GButton playButton;
    private GButton settingButton;
    private GButton quitButton;

    protected override void OnCreateUI()
    {
        playButton = Panel.GetChild("PlayBtn").asButton;
        settingButton = Panel.GetChild("SettingBtn").asButton;
        quitButton = Panel.GetChild("QuitBtn").asButton;
    }

    public void OnPlay(Action action) => playButton.onClick.Add(() => action());
    public void OnSetting(Action action) => settingButton.onClick.Add(() => action());
    public void OnQuit(Action action) => quitButton.onClick.Add(() => action());
}
