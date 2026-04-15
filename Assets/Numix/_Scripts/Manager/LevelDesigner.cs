using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public bool[,] selectedTiles = new bool[10, 10];

    public GameObject tilePrefab;
    public Transform levelParent;

    public float spacing = 1.1f;

    public void GenerateLevel()
    {
        if (tilePrefab == null) return;

        // Clear old level
        if (levelParent != null)
        {
            for (int i = levelParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(levelParent.GetChild(i).gameObject);
            }
        }

        // Spawn selected tiles
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!selectedTiles[x, y]) continue;

                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);

                GameObject tile = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(tilePrefab);
                tile.transform.position = pos;

                if (levelParent != null)
                    tile.transform.SetParent(levelParent);
            }
        }
    }
}