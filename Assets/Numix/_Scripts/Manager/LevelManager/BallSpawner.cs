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
        // we can spawn at little above the  tile position by adding the Vector3.up to the clicked position
        ballInstance = Instantiate(ballPrefab, clicked.position , Quaternion.identity);
    }
}

