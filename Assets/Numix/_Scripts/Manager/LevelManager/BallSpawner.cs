using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject ballInstance;
    private EventBus eventBus;
    private bool isGamePaused;
    private void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>() as EventBus;
    }

    void OnEnable()
    {
        eventBus.Subscribe<Events.OnTileClicked>(SpawnBall);
        eventBus.Subscribe<Events.OnLevelInitialized>(OnLevelInitialized);
        eventBus.Subscribe<Events.OnHomeClicked>(ResetBall);
        eventBus.Subscribe<Events.OnGamePaused>(OnGamePaused);
        eventBus.Subscribe<Events.OnExitButtonClicked>(OnGameResumed);
    }
    void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnTileClicked>(SpawnBall);
        eventBus.Unsubscribe<Events.OnLevelInitialized>(OnLevelInitialized);
        eventBus.Unsubscribe<Events.OnHomeClicked>(ResetBall);
        eventBus.Unsubscribe<Events.OnGamePaused>(OnGamePaused);
        eventBus.Unsubscribe<Events.OnExitButtonClicked>(OnGameResumed);
    }

    void SpawnBall(Events.OnTileClicked clicked)
    {
        if (isGamePaused) return;
        if (ballInstance != null)
        {
            return;
        }
        // we can spawn at little above the  tile position by adding the Vector3.up to the clicked position
        ballInstance = Instantiate(ballPrefab, clicked.position, Quaternion.identity);
    }
    private void ResetBall(Events.OnHomeClicked evt)
    {
        if (ballInstance != null)
        {
            Destroy(ballInstance);
        }
    }
    private void OnLevelInitialized(Events.OnLevelInitialized obj)
    {
        if (ballInstance != null)
        {
            isGamePaused = false;
            Destroy(ballInstance);
        }
        isGamePaused = false;
    }
    private void OnGamePaused(Events.OnGamePaused evt)
    {
        isGamePaused = true;
    }
    private void OnGameResumed(Events.OnExitButtonClicked evt)
    {
        isGamePaused = false;
    }
}

