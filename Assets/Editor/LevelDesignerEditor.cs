using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDesigner))]
public class LevelDesignerEditor : Editor
{
    private TileType selectedType = TileType.Number;
    private int selectedNumber = 1;

    public override void OnInspectorGUI()
    {
        LevelDesigner designer = (LevelDesigner)target;

        DrawDefaultInspector();

        GUILayout.Space(10);

        // Level number input
        designer.levelNumber = EditorGUILayout.IntField("Level Number", designer.levelNumber);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Load / Create Level"))
        {
            designer.LoadOrCreateLevel();
        }

        if (GUILayout.Button("Save Level"))
        {
            designer.SaveByLevelNumber();
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUILayout.Label("Tile Selector", EditorStyles.boldLabel);

        selectedType = (TileType)EditorGUILayout.EnumPopup("Tile Type", selectedType);

        if (selectedType == TileType.Number)
        {
            selectedNumber = EditorGUILayout.IntSlider("Number", selectedNumber, -1, 50);
        }

        GUILayout.Space(10);
        GUILayout.Label("Grid Editor", EditorStyles.boldLabel);

        for (int y = designer.height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < designer.width; x++)
            {
                TileData tile = designer.grid[x, y];

                if (tile == null)
                {
                    tile = new TileData();
                    designer.grid[x, y] = tile;
                }

                Color oldColor = GUI.backgroundColor;

                if (tile.type == TileType.Blocked)
                    GUI.backgroundColor = Color.red;
                else if (tile.type == TileType.Number)
                    GUI.backgroundColor = Color.green;
                else
                    GUI.backgroundColor = Color.gray;

                string label = "";

                if (tile.type == TileType.Number)
                    label = tile.number.ToString();
                else if (tile.type == TileType.Blocked)
                    label = "X";

                if (GUILayout.Button(label, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    tile.type = selectedType;

                    if (selectedType == TileType.Number)
                        tile.number = selectedNumber;
                    else
                        tile.number = 0;

                    EditorUtility.SetDirty(designer);
                }

                GUI.backgroundColor = oldColor;
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Generate Level"))
        {
            designer.GenerateLevel();
        }
    }
}