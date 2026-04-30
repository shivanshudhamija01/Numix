using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static string LEVEL_KEY = "UnlockedLevel";
}
[Serializable]
public class Coordinate
{
    public int x;
    public int z;
    public Coordinate(int x = 0, int z=0)
    {
        this.x = x;
        this.z = z;
    }
};