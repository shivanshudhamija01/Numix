using System.Collections.Generic;
using UnityEngine;

public class GridDataService : IGridDataService
{
    private Dictionary<Vector3, GameObject> positionToTileMap;
    private List<Vector3> numberTilesPositions = new();
    public void MapTileToPosition(Dictionary<Vector3, GameObject> map)
    {
        positionToTileMap = map;
        // numberTilesPositions.Clear();
        // foreach (var kvp in positionToTileMap)
        // {
        //     GameObject tile = kvp.Value;
        //     if (tile.GetComponent<Tile>().TileNumber > 0)
        //     {
        //         numberTilesPositions.Add(kvp.Key);
        //     }
        // }
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
    public void Initialize(IEventBus eventBus)
    {
        eventBus.Subscribe<Events.OnLevelInitialized>(OnLevelInitialized);
    }
    private void OnLevelInitialized(Events.OnLevelInitialized obj)
    {
        numberTilesPositions.Clear();
        foreach (var kvp in positionToTileMap)
        {
            GameObject tile = kvp.Value;
            if (tile.GetComponent<Tile>().TileNumber > 0)
            {
                numberTilesPositions.Add(kvp.Key);
            }
        }
    }
    public List<Vector3> GetNumberTilesPosition()=> numberTilesPositions;
}
