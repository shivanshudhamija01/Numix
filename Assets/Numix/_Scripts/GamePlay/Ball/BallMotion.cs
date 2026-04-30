using System;
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
    private bool isMoving = false;
    private float oscillationTime = 0f;
    private bool canMove = true;

    private IInputService inputService;
    private IMoveValidationService moveValidationService;
    private IStepTrackerService stepTrackerService;
    private IPuzzleValidationService puzzleValidationService;
    private IAudioService audioService;
    private IEventBus eventBus;

    // Here i have to add a logic so that on game win , need to stop the player from accepting the input services.
    void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>();
        ballInitialPosition = new Vector3(transform.position.x, 0, transform.position.z);
        ball = GetComponent<Transform>();

        inputService = ServiceLocator.Get<IInputService>();
        moveValidationService = ServiceLocator.Get<IMoveValidationService>();
        stepTrackerService = ServiceLocator.Get<IStepTrackerService>();
        puzzleValidationService = ServiceLocator.Get<IPuzzleValidationService>();
        audioService = ServiceLocator.Get<IAudioService>();

        moveValidationService.AssingBallCurrentPosition(ballInitialPosition);
        stepTrackerService.IncrementStep();
        puzzleValidationService.EvaluateTile(ballInitialPosition);

        audioService.PlaySFX(SoundType.d1);
    }
    private void OnEnable()
    {
        eventBus.Subscribe<Events.OnMovementStateChanged>(OnGameStateChange);
    }
    private void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnMovementStateChanged>(OnGameStateChange);
    }
    void Update()
    {
        // Let InputService process raw touch data first
        inputService.Update();

        if (!isMoving)
        {
            Oscillate();
            if (!canMove) return;
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
        // Diagonals checked first to take priority over cardinals
        if (inputService.GetForwardLeft()) StartMove(-stepValue, stepValue);
        else if (inputService.GetForwardRight()) StartMove(stepValue, stepValue);
        else if (inputService.GetBackwardLeft()) StartMove(-stepValue, -stepValue);
        else if (inputService.GetBackwardRight()) StartMove(stepValue, -stepValue);

        // Cardinals
        else if (inputService.GetForward()) StartMove(0, stepValue);
        else if (inputService.GetBackward()) StartMove(0, -stepValue);
        else if (inputService.GetLeft()) StartMove(-stepValue, 0);
        else if (inputService.GetRight()) StartMove(stepValue, 0);
    }

    private void StartMove(float xStep, float zStep)
    {
        isMoving = true;
        StartCoroutine(MoveOnSurface(xStep, zStep));
    }

    private IEnumerator MoveOnSurface(float xStep, float zStep)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x + xStep, 0f, startPos.z + zStep);

        if (!moveValidationService.IsValidMove(targetPos))
        {
            isMoving = false;
            yield break;
        }

        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * movementSpeed / stepValue;
            progress = Mathf.Clamp01(progress);

            float baseY = Mathf.Lerp(startPos.y, 0f, progress);
            float bumpY = Mathf.Sin(progress * Mathf.PI) * ballHeight;

            float x = Mathf.Lerp(startPos.x, targetPos.x, progress);
            float z = Mathf.Lerp(startPos.z, targetPos.z, progress);
            float y = Mathf.Clamp(bumpY + baseY, 0f, arcHeight);

            transform.position = new Vector3(x, y, z);
            yield return null;
        }

        transform.position = targetPos;

        moveValidationService.UpdateBallLastPosition(targetPos);
        stepTrackerService.IncrementStep();
        puzzleValidationService.EvaluateTile(targetPos);

        SoundType randomSound = (SoundType)UnityEngine.Random.Range(2, System.Enum.GetValues(typeof(SoundType)).Length);
        audioService.PlaySFX(randomSound);

        oscillationTime = 0f;
        isMoving = false;
    }
    private void OnGameStateChange(Events.OnMovementStateChanged evt)
    {
        canMove = evt.CanMove;
        Debug.Log(canMove);
    }

}