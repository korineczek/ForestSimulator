using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class used for storing information about individual tiles
/// </summary>
[System.Serializable]
public class Tile
{
    //Coordinate systems of tiles
    public Vector2 OffsetCoordinates;
    public Vector3 CubeCoordinates;
    public Vector3 WorldCoordinates;
    
    //Tile properties
    public int Resource = 4;
    public Tree PlacedTree;
    public bool IsActive = false;

    //public Tree PlantedTree;

    public Tile(int col, int row, float height)
    {
        OffsetCoordinates = new Vector2(col,row);
        CubeCoordinates = HexCoords.Offset2Cube(col, row);
        WorldCoordinates = HexCoords.Offset2World(col, height, row);
        //plant debug pine in each tile
        PlacedTree = new Pine();
    }

    public Tile()
    {
        
    }

}
