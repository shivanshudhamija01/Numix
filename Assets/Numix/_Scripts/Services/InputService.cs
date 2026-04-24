using UnityEngine;

public class InputService : IInputService
{
    // Minimum drag distance in pixels to register as a swipe
    private const float SwipeThreshold = 50f;
    // Diagonal detection: if both axis deltas are within this ratio of each other, it's diagonal
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

    // Called every frame from BallMotion.Update()
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

            float absX = Mathf.Abs(delta.x);
            float absY = Mathf.Abs(delta.y);
            float magnitude = delta.magnitude;

            // Not enough drag — treat as tap
            if (magnitude < SwipeThreshold)
            {
                _tap = true;
                return;
            }

            // Normalize both axes relative to the larger one
            float ratioX = absX / magnitude;
            float ratioY = absY / magnitude;

            bool isHorizontal = ratioX > DiagonalRatio;
            bool isVertical = ratioY > DiagonalRatio;

            bool movingRight = delta.x > 0;
            bool movingUp = delta.y > 0;  // Unity screen Y: up = positive

            if (isVertical && isHorizontal)
            {
                // Diagonal
                if (movingUp && movingRight) _forwardRight = true;
                else if (movingUp && !movingRight) _forwardLeft = true;
                else if (!movingUp && movingRight) _backwardRight = true;
                else _backwardLeft = true;
            }
            else if (isVertical)
            {
                // Pure vertical
                if (movingUp) _forward = true;
                else _backward = true;
            }
            else
            {
                // Pure horizontal
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