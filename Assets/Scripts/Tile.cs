using System;
using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
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
    //Physical properties
    public float Slope;

    //Tile properties
    public int Resource = 10;
    public int BaseResource = 10;
    public Tree PlacedTree;
    public bool IsActive = false;
    public bool IsAvailable = true;

    //Physical Tile Controller
    [NonSerialized]
    public TileController Controller;
    [NonSerialized]
    public Transform TreeTransform;

    //Tile buffs
    //TODO: REWORK TO A LOOKUP TABLE OF BUFFS INSTEAD OF THIS STUPID SHIT
    public int[] Buffs = new int[5];
    // 0 - Pine Buff
    // 1 - Oak Buff
    // 2 - Pink Positive
    // 3 - Pink Negative


    //public Tree PlantedTree;

    public Tile(int col, int row, float height)
    {
        OffsetCoordinates = new Vector2(col, row);
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
        if (Buffs[0] > 0 && Buffs[1] == 0)
        {
            finalResource -= Pine.PineUpkeep * Buffs[0];
        }
        else if (Buffs[0] > 0 && Buffs[1] > 0)
        {
            finalResource += (Pine.PineUpkeep - Pine.PineUpkeep * Buffs[0]);
        }
        else if (Buffs[0] == 0 && Buffs[1] > 0)
        {
            finalResource -= Leaf.LeafUpkeep * Buffs[1];
        }
        if (Buffs[2] > 0)
        {
            finalResource += Pink.PinkUpkeep * Buffs[2];
        }
        if (Buffs[3] > 0)
        {
            finalResource -= Pink.PinkUpkeep * Buffs[3];
        }

        //reset wind before checking weather
        switch (GameStats.CurrentWeather)
        {
            case WeatherState.Sunny:
                finalResource += 2;
                break;
            case WeatherState.Raining:
                finalResource += 3 - (int)(WorldCoordinates.y / 5f);
                break;
            case WeatherState.SunClouds:
                finalResource += 1;
                break;
        }
        return finalResource;
    }
}
