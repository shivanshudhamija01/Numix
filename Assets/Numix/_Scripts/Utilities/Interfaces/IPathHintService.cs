
using System.Collections.Generic;
using UnityEngine;

public interface IPathHintService 
{
    // The basic purpose of creating is this service is that , it will store the index of tile, and this index represent the tile number in the solution , so that when the user click on the tile, it will check from then particular tile , and return the index;
    public void Initialize(List<Vector2Int> solutionPath);
    public int GetHintIndex(Coordinate tileIndex);    
}
