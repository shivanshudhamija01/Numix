using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tile;
    [SerializeField] private float stepValue;
    private Coordinate startingPoint;
    private GameObject ballInstance;
    private int[,] matrix = {
        { 0, 0, 3 },
        { 6, 0, 0 },
        { 9, 0, 0 }
    };
    private Dictionary<Vector3, GameObject> positionToTile = new();
    private IMoveValidationService moveValidationService;
    private IGridDataService gridDataService;
    void Awake()
    {
        startingPoint = new Coordinate();
        moveValidationService = ServiceLocator.Get<IMoveValidationService>();
        gridDataService = ServiceLocator.Get<IGridDataService>();
    }
    void Start()
    {
        int row = matrix.GetLength(0);
        int col = matrix.GetLength(1);

        int startX = row / 2;
        int startZ = col / 2;

        startingPoint.x = -1 * startX * stepValue;
        startingPoint.z = -1 * startZ * stepValue;

        Debug.Log("Start point for the row is : " + startingPoint.x + " " + "Start point of the col is : " + startingPoint.z);

        SpawnGrid();
        moveValidationService.MapPositionToTile(positionToTile);
        gridDataService.MapTileToPosition(positionToTile);
    }

    private void SpawnGrid()
    {
        int row = matrix.GetLength(0);
        int col = matrix.GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Vector3 position = new Vector3(startingPoint.x + j * stepValue, 0, startingPoint.z + i * stepValue);
                GameObject Tile = Instantiate(tile, position, Quaternion.identity);
                ITile individualTile = Tile.GetComponent<ITile>();
                individualTile.TileNumber = matrix[i, j];
                positionToTile[position] = Tile;
            }
        }
    }
}
