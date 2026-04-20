

using System;
using FairyGUI;

public class SettingView : ViewBase
{
    protected override string PackageName => "Setting";

    protected override string ComponentName => "SettingPanel";

    private GSlider bgmSlider;
    private GSlider sfxSlider;
    private GButton exitBtn;
    protected override void OnCreateUI()
    {
        bgmSlider = Panel.GetChild("BGMSlider").asSlider;
        sfxSlider = Panel.GetChild("SFXSlider").asSlider;
        exitBtn = Panel.GetChild("ExitBtn").asButton;
    }
    public void OnExit(Action action) => exitBtn.onClick.Add(() => action());
}
