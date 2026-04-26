public interface IStepTrackerService
{
    int CurrentSteps { get; }
    void IncrementStep();
    void ResetSteps();
    void Initialize(IEventBus eventBus);
}