using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class used for storing information about individual tiles
/// </summary>
[System.Serializable]
public class Tile
{
    public enum State
    {
        Healthy, Dying, Dead, Inactive
    }

    //Coordinate systems of tiles
    public Vector2 OffsetCoordinates;
    public Vector3 CubeCoordinates;
    public Vector3 WorldCoordinates;
    //Physical properties
    public float Slope;
    
    //Tile properties
    public int Resource = 10;
    public int BaseResource = 10;
    public Tree PlacedTree;
    public bool IsActive = false;
    public State TileState = State.Inactive;
    

    //Tile buffs
    //TODO: REWORK TO A LOOKUP TABLE OF BUFFS INSTEAD OF THIS STUPID SHIT
    public bool[] Buffs = new bool[5];
    // 0 - Pine Buff
    // 1 - Oak Buff
    // 2 - Pink Positive
    // 3 - Pink Negative


    //public Tree PlantedTree;

    public Tile(int col, int row, float height)
    {
        OffsetCoordinates = new Vector2(col,row);
        CubeCoordinates = HexCoords.Offset2Cube(col, row);
        WorldCoordinates = HexCoords.Offset2World(col, height, row);
    }

    public Tile()
    {
        
    }


    public int EvaluateResource()
    {
        //TODO: CHANGE TO BITMASKING + LOOKUP TABLE
        //run through buffs and evaluate the correct value for the tile
        //reset resource to base resource
        int finalResource = BaseResource;
        //check for pine debuff
        if (Buffs[0] && !Buffs[1])
        {
            finalResource -= Pine.PineUpkeep;
        }
        else if (Buffs[0] && Buffs[1])
        {
            finalResource += Pine.PineUpkeep;
        }
        else if (!Buffs[0] && Buffs[1])
        {
            finalResource -= Leaf.LeafUpkeep;
        }
        if (Buffs[2])
        {
            finalResource += Pink.PinkUpkeep;
        }
        if (Buffs[3])
        {
            finalResource -= Pink.PinkUpkeep;
        }
        return finalResource;
    }
}
