using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveValidationService : IMoveValidationService
{
    private Dictionary<Vector3, GameObject> tileMap = new();
    private Vector3 currentBallPosition;
    private Queue<Vector3> previouslyVisitedTiles = new();


    // Here i am going to map the position of the tile to its game object, so 
    public void MapPositionToTile(Dictionary<Vector3, GameObject> dict)
    {
        ClearReferences();
        tileMap = dict;
    }
    public void AssingBallCurrentPosition(Vector3 position)
    {
        currentBallPosition = position;
    }

    public void UpdateBallLastPosition(Vector3 position)
    {
        previouslyVisitedTiles.Enqueue(currentBallPosition);
        currentBallPosition = position;
    }
    public bool IsValidMove(Vector3 position)
    {
        if (tileMap == null)
        {
            return false;
        }
        if (tileMap.ContainsKey(position) && !IsPreviouslyVisited(position))
        {
            return true;
        }
        return false;
    }
    private bool IsPreviouslyVisited(Vector3 position)
    {
        if (previouslyVisitedTiles.Contains(position))
        {
            return true;
        }
        return false;
    }
    private void ClearReferences()
    {
        previouslyVisitedTiles.Clear();
        // tileMap.Clear();
    }
    public void Initialize(IEventBus eventBus)
    {
        eventBus.Subscribe<Events.OnLevelInitialized>(OnLevelInitialized);
    }
    private void OnLevelInitialized(Events.OnLevelInitialized obj)
    {
        ClearReferences();
    }
}
