using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveValidationService : IMoveValidationService
{
    private Dictionary<Vector3, GameObject> tileMap = new();
    private Vector3 currentBallPosition;
    private Queue<Vector3> previouslyVisitedTiles = new();

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

    public void MapPositionToTile(Dictionary<Vector3, GameObject> dict)
    {
        tileMap = dict;
    }

    public void UpdateBallLastPosition(Vector3 position)
    {
        Debug.Log("Ball last position is : " + currentBallPosition);
        Debug.Log("Ball current position is : " + position);
        previouslyVisitedTiles.Enqueue(currentBallPosition);
        currentBallPosition = position;
    }
    public void AssingBallCurrentPosition(Vector3 position)
    {
        currentBallPosition = position;
    }
    private bool IsPreviouslyVisited(Vector3 position)
    {
        if (previouslyVisitedTiles.Contains(position))
        {
            return true;
        }
        return false;
    }
}
