public abstract class PresenterBase<TView> : IPresenter where TView : ViewBase
{
    protected readonly TView view;
    protected readonly IEventBus eventBus;

    protected PresenterBase(TView view, IEventBus eventBus)
    {
        this.view = view;
        this.eventBus = eventBus;
    }

    public virtual void Initialize()
    {

    }

}