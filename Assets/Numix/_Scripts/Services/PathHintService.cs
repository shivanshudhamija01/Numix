
using System.Collections.Generic;
using UnityEngine;

public class PathHintService : IPathHintService
{
    private List<Vector2Int> solutionPath;
    public void Initialize(List<Vector2Int> solutionPath)
    {
        this.solutionPath = solutionPath;
    }
    public int GetHintIndex(Coordinate tileIndex)
    {
        if (solutionPath == null || solutionPath.Count == 0)
        {
            return -1;
        }
        Vector2Int tileCoord = new Vector2Int(tileIndex.x, tileIndex.z);
        if (solutionPath.Contains(tileCoord))
        {
            return solutionPath.IndexOf(tileCoord) + 1;
        }
        return -1; // Return -1 if the tile is not part of the solution path
    }

}
