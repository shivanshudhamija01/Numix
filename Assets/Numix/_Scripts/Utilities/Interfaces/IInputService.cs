public interface IInputService
{
    bool GetTap();
    bool GetForward();
    bool GetBackward();
    bool GetLeft();
    bool GetRight();
    bool GetForwardLeft();
    bool GetForwardRight();
    bool GetBackwardLeft();
    bool GetBackwardRight();
    void Update(); // Called manually from BallMotion to process touch each frame
}