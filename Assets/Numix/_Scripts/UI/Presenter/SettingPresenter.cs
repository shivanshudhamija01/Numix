using UnityEngine;

public class SettingPresenter : PresenterBase<SettingView>
{
    public SettingPresenter(SettingView view, IEventBus eventBus) : base(view, eventBus)
    {

    }
    public override void Initialize()
    {
        view.CreateUI();
        view.OnExit(() =>
        {
            eventBus.Publish(new Events.OnExitButtonClicked());
            Debug.Log("Exit button is clicked");
        });
    }
}
