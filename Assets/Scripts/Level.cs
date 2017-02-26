using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public List<Tile> Map;

    public Level()
    {
        
    }

    public Level(Tile[,] tiles)
    {
        Map = new List<Tile>();
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Map.Add(tiles[i,j]);
            }
        }
        Debug.Log(Map.Count);
    }
}