using System.Collections.Generic;
using UnityEngine;

public class GridDataService : IGridDataService
{
    private Dictionary<Vector3, GameObject> positionToTileMap;

    public void MapTileToPosition(Dictionary<Vector3, GameObject> map)
    {
        positionToTileMap = map;
    }
    public int GetTileNumber(Vector3 position)
    {
        if (positionToTileMap != null)
        {
            GameObject tile = positionToTileMap[position];
            int numberOnTile = tile.GetComponent<Tile>().TileNumber;
            return numberOnTile;
        }
        return 0;
    }

}
