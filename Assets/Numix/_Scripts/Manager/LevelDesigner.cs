using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LevelDesigner : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public int levelNumber = 1;

    public TileData[,] grid = new TileData[10, 10];

    public GameObject numberTilePrefab;
    public GameObject blockedTilePrefab;

    public Transform levelParent;
    public float spacing = 1.1f;

    public LevelData currentLevelData;

    //Solution Path
    public List<Vector2Int> solutionPath = new List<Vector2Int>();

    void Awake()
    {
        Debug.Log("Solution path is : " + solutionPath.Count);
        for (int i = 0; i < solutionPath.Count; i++)
        {
            Debug.Log($"Tile {i}th is at : {solutionPath[i]}");
        }
    }
    private void OnValidate()
    {
        if (width <= 0) width = 1;
        if (height <= 0) height = 1;

        if (grid == null)
        {
            grid = new TileData[width, height];
        }

        if (grid.GetLength(0) != width || grid.GetLength(1) != height)
        {
            TileData[,] newGrid = new TileData[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x < grid.GetLength(0) && y < grid.GetLength(1))
                        newGrid[x, y] = grid[x, y];
                    else
                        newGrid[x, y] = new TileData();
                }
            }

            grid = newGrid;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == null)
                    grid[x, y] = new TileData();
            }
        }
    }

    private string GetLevelPath()
    {
        return $"Assets/Levels/Level_{levelNumber}.asset";
    }

    public void LoadOrCreateLevel()
    {
#if UNITY_EDITOR
        if (!AssetDatabase.IsValidFolder("Assets/Levels"))
        {
            AssetDatabase.CreateFolder("Assets", "Levels");
        }

        string path = GetLevelPath();
        LevelData data = AssetDatabase.LoadAssetAtPath<LevelData>(path);

        if (data == null)
        {
            data = ScriptableObject.CreateInstance<LevelData>();

            data.width = width;
            data.height = height;
            data.grid = new TileData[width * height];

            for (int i = 0; i < data.grid.Length; i++)
                data.grid[i] = new TileData();

            data.solutionPath = new List<Vector2Int>();

            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.SaveAssets();

            Debug.Log($"Created new level: {path}");
        }
        else
        {
            Debug.Log($"Loaded existing level: {path}");
        }

        currentLevelData = data;
        LoadLevel(data);
#endif
    }

    public void LoadLevel(LevelData data)
    {
        if (data == null) return;

        width = data.width;
        height = data.height;

        grid = new TileData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = x + y * width;

                TileData source = data.grid[index];

                TileData copy = new TileData();
                copy.type = source.type;
                copy.number = source.number;

                grid[x, y] = copy;
            }
        }

        // 🔥 NEW: Load path
        solutionPath = new List<Vector2Int>(data.solutionPath);
    }

    public void SaveByLevelNumber()
    {
#if UNITY_EDITOR
        if (currentLevelData == null)
        {
            Debug.LogError("No LevelData loaded!");
            return;
        }

        currentLevelData.width = width;
        currentLevelData.height = height;
        currentLevelData.grid = new TileData[width * height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = x + y * width;

                TileData source = grid[x, y];

                TileData copy = new TileData();
                copy.type = source.type;
                copy.number = source.number;

                currentLevelData.grid[index] = copy;
            }
        }

        // 🔥 NEW: Save path
        currentLevelData.solutionPath = new List<Vector2Int>(solutionPath);

        EditorUtility.SetDirty(currentLevelData);
        AssetDatabase.SaveAssets();

        Debug.Log($"Saved Level {levelNumber}");
#endif
    }

    public void GenerateLevel()
    {
#if UNITY_EDITOR
        if (levelParent != null)
        {
            for (int i = levelParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(levelParent.GetChild(i).gameObject);
            }
        }

        float offsetX = (width - 1) * spacing * 0.5f;
        float offsetZ = (height - 1) * spacing * 0.5f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileData tileData = grid[x, y];

                if (tileData == null || tileData.type == TileType.Empty)
                    continue;

                Vector3 pos = new Vector3(
                    x * spacing - offsetX,
                    0,
                    y * spacing - offsetZ
                );

                GameObject tile = null;

                if (tileData.type == TileType.Blocked)
                {
                    tile = (GameObject)PrefabUtility.InstantiatePrefab(blockedTilePrefab);
                }
                else if (tileData.type == TileType.Number)
                {
                    tile = (GameObject)PrefabUtility.InstantiatePrefab(numberTilePrefab);

                    var tileScript = tile.GetComponent<Tile>();
                    if (tileScript != null)
                    {
                        tileScript.TileNumber = Mathf.Clamp(tileData.number, -1, 50);
                    }
                }

                if (tile == null) continue;

                tile.transform.position = pos;

                if (levelParent != null)
                    tile.transform.SetParent(levelParent);
            }
        }
#endif
    }
}