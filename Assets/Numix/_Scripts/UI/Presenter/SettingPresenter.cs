public class SettingPresenter : PresenterBase<SettingView>
{
    public SettingPresenter(SettingView view, IEventBus eventBus) : base(view, eventBus)
    {

    }
    public override void Initialize()
    {
        View.CreateUI();
    }
}
