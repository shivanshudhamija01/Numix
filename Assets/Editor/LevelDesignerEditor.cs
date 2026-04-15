using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDesigner))]
public class LevelDesignerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelDesigner designer = (LevelDesigner)target;

        DrawDefaultInspector();

        GUILayout.Space(10);
        GUILayout.Label("Grid Editor", EditorStyles.boldLabel);

        for (int y = designer.height - 1; y >= 0; y--)
        {
            GUILayout.BeginHorizontal();

            for (int x = 0; x < designer.width; x++)
            {
                Color oldColor = GUI.backgroundColor;

                GUI.backgroundColor = designer.selectedTiles[x, y]
                    ? Color.green
                    : Color.gray;

                if (GUILayout.Button("", GUILayout.Width(25), GUILayout.Height(25)))
                {
                    designer.selectedTiles[x, y] =
                        !designer.selectedTiles[x, y];

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