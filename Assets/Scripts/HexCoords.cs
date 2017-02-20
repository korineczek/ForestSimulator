using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class HexCoords
{
    public static readonly float HEIGHT = 0.728f;

    public static void Axial2Cube()
    {
        
    }

    public static void Cube2Axial()
    {
        
    }

    public static void Axial2World()
    {
        
    }

    public static void World2Axial()
    {
        
    }

    public static void Cube2World()
    {
        
    }

    public static void World2Cube()
    {
        
    }

    public static Vector3 Offset2World(int x, int y, int z)
    {
        float width = (Mathf.Sqrt(3f)/2f)*HEIGHT;
        float xCoord = x*width + (z%2 == 0 ? width/2f : width);
        float zCoord = z*HEIGHT;
        return new Vector3(xCoord, y, zCoord);
    }


}

