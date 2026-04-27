using UnityEngine;

public interface ITile
{
    public int TileNumber { get; set; }
    public Coordinate index { get; set; }
}
