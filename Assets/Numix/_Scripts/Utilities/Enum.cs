using System;
public enum SoundType
{
    BGM,
    click,
    d1,
    e1,
    f,
    g

}
public enum TileType
{
    Empty,
    Number,
    Blocked
}

[Serializable]
public class TileData
{
    public TileType type;
    public int number;
}

// a,
// b,
// c,
// c2,