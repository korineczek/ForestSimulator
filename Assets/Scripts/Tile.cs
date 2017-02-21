using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class used for storing information about individual tiles
/// </summary>
public class Tile
{

    public Vector2 OffsetCoordinates;
    public Vector3 CubeCoordinates;
    public Vector3 WorldCoordinates;

    public Tile(int col, int row, int height)
    {
        OffsetCoordinates = new Vector2(col,row);
        CubeCoordinates = HexCoords.Offset2Cube(col, row);
        WorldCoordinates = HexCoords.Offset2World(col, height, row);
    }

    public Tile()
    {
        
    }

}
