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
}