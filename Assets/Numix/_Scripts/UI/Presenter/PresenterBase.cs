public abstract class PresenterBase<TView> : IPresenter where TView : ViewBase
{
    protected readonly TView View;
    protected readonly EventBus EventBus;

    protected PresenterBase(TView view, EventBus eventBus)
    {
        View = view;
        EventBus = eventBus;
    }

    public virtual void Initialize()
    {

    }

}