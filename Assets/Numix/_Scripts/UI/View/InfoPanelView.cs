

using System;
using FairyGUI;

public class InfoPanelView : ViewBase
{
    protected override string PackageName => "Info";

    protected override string ComponentName => "InfoPanel";

    private GButton exitButton;

    protected override void OnCreateUI()
    {
        exitButton = Panel.GetChild("ExitBtn").asButton;
    }

    public void OnExit(Action action) => exitButton.onClick.Add(() => action());
}
