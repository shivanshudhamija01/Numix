using FairyGUI;
public class LevelSelectorView : ViewBase
{
    protected override string PackageName => "LevelSelector";
    protected override string ComponentName => "LevelSelectorPanel";

    private GList levelList;
    private int totalLevels;

    public System.Action<int> OnLevelClicked;

    protected override void OnCreateUI()
    {
        levelList = Panel.GetChild("List").asList;
        levelList.SetVirtual();
        levelList.itemRenderer = SetRenderItem;
    }

    private void SetRenderItem(int index, GObject obj)
    {
        GComponent item = obj.asCom;

        GTextField levelTxt = item.GetChild("Txt").asTextField;
        GObject lockIcon = item.GetChild("Lock");
        GButton levelButton = item.asButton;

        int levelIndex = index + 1;

        levelTxt.text = levelIndex.ToString();
        lockIcon.visible = false;

        levelButton.onClick.Clear();

        levelButton.onClick.Add(() =>
        {
            OnLevelClicked?.Invoke(levelIndex);
        });
    }

    public override void Show()
    {
        base.Show();
        levelList.numItems = totalLevels;
        levelList.RefreshVirtualList();
    }

    public void SetLevel(int value) => totalLevels = value;
}