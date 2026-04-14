using System.Collections;
using UnityEngine;

public class BallMotion : MonoBehaviour
{
    [SerializeField] private float oscillationSpeed = 1f;
    [SerializeField] private float ballHeight = 4f;
    [SerializeField] private float stepValue = 1.25f;
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float arcHeight = 2f;

    private Vector3 ballInitialPosition;
    private Transform ball;
    private bool isMoving;
    private float oscillationTime;
    private IInputService inputService;
    private IMoveValidationService moveValidationService;
    private IStepTrackerService stepTrackerService;
    private IPuzzleValidationService puzzleValidationService;
    private IAudioService audioService;

    void Awake()
    {
        ballInitialPosition = new Vector3(transform.position.x, 0, transform.position.z);
        ball = GetComponent<Transform>();
        inputService = ServiceLocator.Get<IInputService>();
        moveValidationService = ServiceLocator.Get<IMoveValidationService>();
        stepTrackerService = ServiceLocator.Get<IStepTrackerService>();
        puzzleValidationService = ServiceLocator.Get<IPuzzleValidationService>();
        moveValidationService.AssingBallCurrentPosition(ballInitialPosition);
        stepTrackerService.IncrementStep();
        puzzleValidationService = ServiceLocator.Get<IPuzzleValidationService>();
        puzzleValidationService.EvaluateTile(ballInitialPosition);
        audioService = ServiceLocator.Get<IAudioService>();
        audioService.PlaySFX(SoundType.d1);
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
        if (inputService.GetForward())
            StartMove(0, stepValue);
        else if (inputService.GetBackward())
            StartMove(0, -stepValue);
        else if (inputService.GetLeft())
            StartMove(-stepValue, 0);
        else if (inputService.GetRight())
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

        // if face any issue in targetPos matching , then use the rounded target
        // Vector3 roundedTarget = new Vector3(Mathf.Round(targetPos.x * 100f) / 100f, 0f, Mathf.Round(targetPos.z * 100f) / 100f);

        if (!moveValidationService.IsValidMove(targetPos))
        {
            Debug.Log("Not a valid move");
            isMoving = false;
            yield break;
        }
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
        moveValidationService.UpdateBallLastPosition(targetPos);
        stepTrackerService.IncrementStep();
        puzzleValidationService.EvaluateTile(targetPos);
        SoundType randomSound = (SoundType)Random.Range(0, System.Enum.GetValues(typeof(SoundType)).Length);
        audioService.PlaySFX(randomSound);
        oscillationTime = 0f;
        isMoving = false;
    }
}