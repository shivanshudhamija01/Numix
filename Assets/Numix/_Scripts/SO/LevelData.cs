using UnityEngine;
[CreateAssetMenu(menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    public int width;
    public int height;
    public TileData[] grid;
}