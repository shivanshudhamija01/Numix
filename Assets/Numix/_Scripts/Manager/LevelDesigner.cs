using UnityEngine;
using UnityEditor;

public class LevelDesigner : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public TileData[,] grid = new TileData[10, 10];

    public GameObject numberTilePrefab;
    public GameObject blockedTilePrefab;

    public Transform levelParent;
    public float spacing = 1.1f;

    private void OnValidate()
    {
        if (grid == null || grid.Length == 0)
            grid = new TileData[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == null)
                    grid[x, y] = new TileData();
            }
        }
    }

    public void GenerateLevel()
    {
        // Clear old level
        if (levelParent != null)
        {
            for (int i = levelParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(levelParent.GetChild(i).gameObject);
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileData tileData = grid[x, y];

                if (tileData == null || tileData.type == TileType.Empty)
                    continue;

                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);

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
    }
}