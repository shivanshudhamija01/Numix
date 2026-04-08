using System.Collections;
using UnityEngine;

public class BallMotion : MonoBehaviour
{
    [SerializeField] private float oscillationSpeed = 1f;
    [SerializeField] private float ballHeight = 4f;
    [SerializeField] private float stepValue = 1.25f;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float arcHeight = 2f;

    private Transform ball;
    private bool isMoving;
    private float oscillationTime;

    void Awake()
    {
        ball = GetComponent<Transform>();
    }

    void Update()
    {
        if (!isMoving)
        {
            Oscillate();
            HandleInput();
        }
    }

    private void Oscillate()
    {
        oscillationTime += Time.deltaTime;
        float y = Mathf.Abs(Mathf.Sin(oscillationTime * oscillationSpeed)) * ballHeight;
        ball.position = new Vector3(ball.position.x, y, ball.position.z);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            StartMove(0, stepValue);
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            StartMove(0, -stepValue);
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            StartMove(-stepValue, 0);
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            StartMove(stepValue, 0);
    }

    private void StartMove(float xStep, float zStep)
    {
        isMoving = true;
        StartCoroutine(MoveOnSurface(xStep, zStep));
    }

    private IEnumerator MoveOnSurface(float xStep, float zStep)
    {
        Vector3 startPos = transform.position; // captures real Y at moment of input

        Vector3 targetPos = new Vector3(startPos.x + xStep, 0f, startPos.z + zStep);

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * movementSpeed / stepValue;
            progress = Mathf.Clamp01(progress);

            // Bring startY down to 0 linearly as progress goes 0→1
            float baseY = Mathf.Lerp(startPos.y, 0f, progress);

            // Parabola bump on top — Sin(PI) = 0 so it doesn't affect the landing
            float bumpY = Mathf.Sin(progress * Mathf.PI) * ballHeight;

            float x = Mathf.Lerp(startPos.x, targetPos.x, progress);
            float z = Mathf.Lerp(startPos.z, targetPos.z, progress);

            float y = Mathf.Clamp(bumpY + baseY, 0, arcHeight);
            transform.position = new Vector3(x, y, z);

            yield return null;
        }

        transform.position = targetPos;

        oscillationTime = 0f;
        isMoving = false;
    }
}