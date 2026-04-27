using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject numberTilePrefab;
    [SerializeField] private GameObject blockedTilePrefab;
    [SerializeField] private float spacing = 1.25f;
    private IEventBus eventBus;
    private IMoveValidationService moveValidationService;
    private IGridDataService gridDataService;
    private IGameServices gameServices;
    private IPuzzleValidationService puzzleValidationService ;
    private IPathHintService pathHintService;
    private Dictionary<Vector3, GameObject> positionToTile = new();
    public void Initialize(IEventBus eventBus, IMoveValidationService moveValidationService, IGridDataService gridDataService, IGameServices gameServices, IPuzzleValidationService puzzleValidationService, IPathHintService pathHintService)
    {
        this.eventBus = eventBus;
        this.moveValidationService = moveValidationService;
        this.gridDataService = gridDataService;
        this.gameServices = gameServices;
        this.puzzleValidationService = puzzleValidationService;
        this.pathHintService = pathHintService;
        eventBus.Subscribe<Events.OnLoadLevel>(OnLoadLevel);
    }
    private  void OnLoadLevel(Events.OnLoadLevel evt)
    {   
        LoadLevel(evt.levelIndex);
    }
    private void LoadLevel(int levelIndex)
    {
        Debug.Log("Loading Level: " + levelIndex);
        LevelData levelData = Resources.Load<LevelData>($"Levels/Level_{levelIndex}");
        positionToTile.Clear();
        if(levelData == null)
        {
            Debug.LogError($"Level {levelIndex} not found!");
            return;
        }
        GenerateLevel(levelData);
        gameServices.CurrentLevel = levelIndex;
        moveValidationService.MapPositionToTile(positionToTile);
        gridDataService.MapTileToPosition(positionToTile);  
        pathHintService.Initialize(levelData.solutionPath);
        eventBus.Publish(new Events.OnLevelInitialized());
    
    }
    private void GenerateLevel(LevelData levelData)
    {
        foreach(Transform child in levelParent)
        {
            Destroy(child.gameObject);
        }
        float offsetX = (levelData.width - 1) * spacing * 0.5f;
        float offsetZ = (levelData.height - 1) * spacing * 0.5f;

        for (int x = 0; x < levelData.width; x++)
        {
            for (int y = 0; y < levelData.height; y++)
            {
                int index = x + y * levelData.width;
                TileData tile = levelData.grid[index];

                if (tile.type == TileType.Empty) continue;

                Vector3 pos = new Vector3(
                    x * spacing - offsetX,
                    0,
                    y * spacing - offsetZ
                );

                GameObject obj = null;

                if (tile.type == TileType.Blocked)
                {
                    obj = Instantiate(blockedTilePrefab);
                }
                else
                {
                    obj = Instantiate(numberTilePrefab);
                    Tile cell = obj.GetComponent<Tile>();
                    cell.TileNumber = tile.number;
                    cell.index = new Coordinate(x, y);
                }

                obj.transform.position = pos;
                positionToTile[pos] = obj;
                obj.transform.SetParent(levelParent);
            }
        }
    }
    public void OnDestroy()
    {
        eventBus.Unsubscribe<Events.OnLoadLevel>(OnLoadLevel);
    }
}
