using FairyGUI;
public abstract class ViewBase
{
    protected abstract string PackageName { get; }
    protected abstract string ComponentName { get; }

    protected GComponent Panel { get; private set; }

    public void CreateUI()
    {
        UIPackage.AddPackage(PackageName);
        Panel = UIPackage.CreateObject(PackageName, ComponentName).asCom;
        GRoot.inst.AddChild(Panel);
        Panel.SetSize(GRoot.inst.width, GRoot.inst.height);
        Panel.AddRelation(GRoot.inst, RelationType.Size);

        OnCreateUI();
    }

    protected virtual void OnCreateUI() { }
    public virtual void Show()
    {
        if (Panel != null)
        {
            Panel.visible = true;
        }
    }
    public virtual void Hide()
    {
        if (Panel != null)
        {
            Panel.visible = false;
        }
    }
}