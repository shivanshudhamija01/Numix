public class StepTrackerService : IStepTrackerService
{
    public int CurrentSteps { get; private set; }

    public void IncrementStep()
    {
        CurrentSteps++;
    }

    public void ResetSteps()
    {
        CurrentSteps = 0;
    }
    public void Initialize(IEventBus eventBus)
    {
        eventBus.Subscribe<Events.OnLevelInitialized>(OnLevelInitialized);
    }
    private void OnLevelInitialized(Events.OnLevelInitialized obj)
    {
        ResetSteps();
    }
}