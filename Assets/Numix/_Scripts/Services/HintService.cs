

public class HintService : IHintService
{
    private bool isHintActive;
    private int remainingHints;

    public bool IsHintActive { get => isHintActive; set => isHintActive = value; }
    public int RemainingHints { get => remainingHints; set => remainingHints = value; }
}
