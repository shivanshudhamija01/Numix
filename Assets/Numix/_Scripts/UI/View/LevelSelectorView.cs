using FairyGUI;
using UnityEngine;
public class LevelSelectorView : ViewBase
{
    protected override string PackageName => "LevelSelector";
    protected override string ComponentName => "LevelSelectorPanel";

    private GList levelList;
    private int totalLevels;
    private int unlockedLevel;
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

        if(levelIndex <= unlockedLevel)
        {
            lockIcon.visible = false;
            levelButton.touchable = true;
        }
        else
        {
            lockIcon.visible = true;
            levelButton.touchable = false;
        }

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
        unlockedLevel = PlayerPrefs.GetInt(Utility.LEVEL_KEY, 1);
        levelList.RefreshVirtualList();
    }

    public void SetLevel(int value) => totalLevels = value;
}