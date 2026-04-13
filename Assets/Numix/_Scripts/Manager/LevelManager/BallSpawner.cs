using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameObject ballInstance;
    private EventBus eventBus;

    private void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>() as EventBus;
    }

    void OnEnable()
    {
        eventBus.Subscribe<Events.OnTileClicked>(SpawnBall);
    }
    void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnTileClicked>(SpawnBall);
    }

    void SpawnBall(Events.OnTileClicked clicked)
    {
        if (ballInstance != null)
        {
            return;
        }
        ballInstance = Instantiate(ballPrefab, clicked.position + Vector3.up, Quaternion.identity);
    }
}
