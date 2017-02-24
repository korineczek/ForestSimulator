using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Reference class containing technical back end fucntions that handle conversion between different coordinate systems
/// </summary>
internal static class HexCoords
{
    public static readonly float HEIGHT = 1f;

    /// <summary>
    /// Conversion between offset coordinates and world coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Offset2World(int x, float y, int z)
    {
        float width = (Mathf.Sqrt(3f)/2f)*HEIGHT;
        float xCoord = x*width + (z%2 == 0 ? width/2f : width);
        float zCoord = z*0.75f; //TODO: fix this so it is not hardcoded
        return new Vector3(xCoord, y, zCoord);
    }

    /// <summary>
    /// Convert betwetween world position 
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector2 World2Offset(Vector3 pos)
    {
        float q = (pos.x*(float) Math.Sqrt(3)/3f - pos.z/3f) / (HEIGHT/2f);
        float r = pos.z*2f/3f/(HEIGHT/2f);
        return Cube2Offset(RoundToNearest(new Vector3(q, -q - r, r)));
    }

    /// <summary>
    /// Round floating point hex coords because reasons
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static Vector3 RoundToNearest(Vector3 hex)
    {
        int rx = (int) hex.x;
        int ry = (int) hex.y;
        int rz = (int) hex.z;

        float xdiff = Mathf.Abs(rx - hex.x);
        float ydiff = Mathf.Abs(ry - hex.y);
        float zdiff = Mathf.Abs(rz - hex.z);

        //this is where the magic happens :^)
        if (xdiff > ydiff && xdiff > zdiff)
        {
            rx = -ry - rz;
        }
        else if (ydiff > zdiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }
        return new Vector3(rx, ry, rz);
    }



    /*
    public static Vector3 RoundToNearest(Vector3 hex)
    {
        
    }
     */

    /// <summary>
    /// Conversion between offset coordinates and cube coordinates
    /// </summary>
    /// <param name="col"></param>
    /// <param name="row"></param>
    /// <returns></returns>
    public static Vector3 Offset2Cube(int col, int row)
    {
        float x, y, z;
        x = col - (row - (row & 1))/2;
        z = row;
        y = -x - z;
        return new Vector3(x, y, z);
    }

    /// <summary>
    /// Conversion between cube coordinates and offset coordinates
    /// </summary>
    /// <param name="cube"></param>
    /// <returns></returns>
    public static Vector2 Cube2Offset(Vector3 cube)
    {
        int col = (int)cube.x + ((int)cube.z - ((int)cube.z & 1)) / 2;
        int row = (int)cube.z;
        return new Vector2(col, row);
    }

    /// <summary>
    /// distance between two hexes from offset coordinates
    /// </summary>
    /// <param name="col1"></param>
    /// <param name="row1"></param>
    /// <param name="col2"></param>
    /// <param name="row2"></param>
    /// <returns></returns>
    public static float HexDistance(int col1, int row1, int col2, int row2)
    {

        return HexDistance(Offset2Cube(col1, row1), Offset2Cube(col2, row2));

    }

    /// <summary>
    /// distance between two hexes from cube coordinates
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float HexDistance(Vector3 a, Vector3 b)
    {

        return (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z))/2;

    }

    /// <summary>
    /// Use to get all affected hexes within a specific range from a certain hex
    /// </summary>
    /// <param name="p"></param>
    /// <param name="range"></param>
    /// <returns>All affected hexes within range</returns>
    public static List<Vector3> HexRange(Vector3 p, int range)
    {
        List<Vector3> results = new List<Vector3>();


        for (int i = (int)p.x-range; i <= (int)p.x+range; i++)
        {
            for (int j = (int)p.y - range; j <= (int)p.y + range; j++)
            {
                for (int k  = (int)p.z - range; k <= (int)p.z + range; k++) //TODO: optimize this so we dont have the redundant for loop
                {
                    if (i + j + k == 0)
                    {
                        results.Add(new Vector3(i,j,k));
                    }
                }
            }    
        }
        return results;
    } 
}

