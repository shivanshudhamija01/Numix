public abstract class PresenterBase<TView> : IPresenter where TView : ViewBase
{
    protected readonly TView view;
    protected readonly IEventBus eventBus;
    protected readonly IAudioService audioService;

    protected PresenterBase(TView view, IEventBus eventBus, IAudioService audioService)
    {
        this.view = view;
        this.eventBus = eventBus;
        this.audioService = audioService;
    }

    public virtual void Initialize()
    {

    }
    public virtual void Dispose()
    {

    }
}