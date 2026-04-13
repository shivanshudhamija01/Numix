using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveValidationService
{
    public void MapPositionToTile(Dictionary<Vector3, GameObject> dict);
    public bool IsValidMove(Vector3 position);
    public void UpdateBallLastPosition(Vector3 position);
    public void AssingBallCurrentPosition(Vector3 position);
}
