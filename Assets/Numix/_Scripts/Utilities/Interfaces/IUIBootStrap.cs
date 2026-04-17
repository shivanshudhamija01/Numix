using System.Collections.Generic;

public interface IUIBootStrap
{
    public void Initialize();
    public List<IPresenter> GetPresenterList();
    public List<ViewBase> GetViewsList();
}
