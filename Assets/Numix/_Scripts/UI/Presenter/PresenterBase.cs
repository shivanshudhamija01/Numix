public abstract class PresenterBase<TView> : IPresenter where TView : ViewBase
{
    protected readonly TView View;
    protected readonly IEventBus EventBus;

    protected PresenterBase(TView view, IEventBus eventBus)
    {
        View = view;
        EventBus = eventBus;
    }

    public virtual void Initialize()
    {

    }

}