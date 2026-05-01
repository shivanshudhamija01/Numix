

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

    public void OnBGMSliderChanged(Action<float> action)
    {
        bgmSlider.onChanged.Add(() =>
        {
            action((float)bgmSlider.value / 100f);
        });
    }

    public void OnSFXSliderChanged(Action<float> action)
    {
        sfxSlider.onChanged.Add(() =>
        {
            action((float)sfxSlider.value / 100f);
        });
    }
    public void SetInitialValues(float bgm, float sfx)
    {
        bgmSlider.value = bgm * 100f;
        sfxSlider.value = sfx * 100f;
    }
}
