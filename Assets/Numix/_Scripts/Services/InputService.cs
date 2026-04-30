using UnityEngine;

public class InputService : IInputService
{
    private const float SwipeThreshold = 50f;
    private const float DiagonalRatio = 0.4f;

    private Vector2 touchStartPos;
    private bool touchStarted = false;

    private bool _tap = false;
    private bool _forward = false;
    private bool _backward = false;
    private bool _left = false;
    private bool _right = false;
    private bool _forwardLeft = false;
    private bool _forwardRight = false;
    private bool _backwardLeft = false;
    private bool _backwardRight = false;

    public void Update()
    {
        ResetFlags();

        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStartPos = touch.position;
            touchStarted = true;
        }
        else if (touch.phase == TouchPhase.Ended && touchStarted)
        {
            touchStarted = false;
            Vector2 delta = touch.position - touchStartPos;

            // Rotate delta by -45° to compensate for isometric camera rotation
            float angle = -45f * Mathf.Deg2Rad;
            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);
            Vector2 rotatedDelta = new Vector2(
                delta.x * cos - delta.y * sin,
                delta.x * sin + delta.y * cos
            );

            float absX = Mathf.Abs(rotatedDelta.x);
            float absY = Mathf.Abs(rotatedDelta.y);
            float magnitude = rotatedDelta.magnitude;

            if (magnitude < SwipeThreshold)
            {
                _tap = true;
                return;
            }

            float ratioX = absX / magnitude;
            float ratioY = absY / magnitude;

            bool isHorizontal = ratioX > DiagonalRatio;
            bool isVertical = ratioY > DiagonalRatio;

            bool movingRight = rotatedDelta.x > 0;
            bool movingUp = rotatedDelta.y > 0;

            if (isVertical && isHorizontal)
            {
                if (movingUp && movingRight) _forwardRight = true;
                else if (movingUp && !movingRight) _forwardLeft = true;
                else if (!movingUp && movingRight) _backwardRight = true;
                else _backwardLeft = true;
            }
            else if (isVertical)
            {
                if (movingUp) _forward = true;
                else _backward = true;
            }
            else
            {
                if (movingRight) _right = true;
                else _left = true;
            }
        }
    }

    private void ResetFlags()
    {
        _tap = false;
        _forward = false;
        _backward = false;
        _left = false;
        _right = false;
        _forwardLeft = false;
        _forwardRight = false;
        _backwardLeft = false;
        _backwardRight = false;
    }

    public bool GetTap() => _tap;
    public bool GetForward() => _forward;
    public bool GetBackward() => _backward;
    public bool GetLeft() => _left;
    public bool GetRight() => _right;
    public bool GetForwardLeft() => _forwardLeft;
    public bool GetForwardRight() => _forwardRight;
    public bool GetBackwardLeft() => _backwardLeft;
    public bool GetBackwardRight() => _backwardRight;
}