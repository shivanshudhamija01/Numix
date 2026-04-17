
using System;
using FairyGUI;

public class GameLostView : ViewBase
{
    protected override string PackageName => "GameLost";

    protected override string ComponentName => "GameLostPanel";

    private GButton restartBtn;
    private GButton homeBtn;
    protected override void OnCreateUI()
    {
        restartBtn = Panel.GetChild("RestartBtn").asButton;
        homeBtn = Panel.GetChild("HomeBtn").asButton;
    }

    public void OnRestart(Action action) => restartBtn.onClick.Add(() => action());
    public void OnHome(Action action) => homeBtn.onClick.Add(() => action());
}
