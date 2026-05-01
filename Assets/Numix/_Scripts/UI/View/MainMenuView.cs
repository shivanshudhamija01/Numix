using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class MainMenuView : ViewBase
{
    protected override string PackageName => "MainMenu";
    protected override string ComponentName => "MainMenuPanel";
    private GButton playButton;
    private GButton settingButton;
    private GButton quitButton;
    private GButton infoButton;

    protected override void OnCreateUI()
    {
        playButton = Panel.GetChild("PlayBtn").asButton;
        settingButton = Panel.GetChild("SettingBtn").asButton;
        quitButton = Panel.GetChild("QuitBtn").asButton;
        infoButton = Panel.GetChild("InfoBtn").asButton;
    }

    public void OnPlay(Action action) => playButton.onClick.Add(() => action());
    public void OnSetting(Action action) => settingButton.onClick.Add(() => action());
    public void OnQuit(Action action) => quitButton.onClick.Add(() => action());
    public void OnInfo(Action action) => infoButton.onClick.Add(() => action());
}
