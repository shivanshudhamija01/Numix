using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    public int width;
    public int height;
    public TileData[] grid;
    public List<Vector2Int> solutionPath = new List<Vector2Int>();
    public float CameraFOV;
    public Vector3 CameraPos;

}