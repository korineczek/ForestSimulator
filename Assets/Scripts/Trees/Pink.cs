﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pink : Tree
{
    private int[] aoeMaskPositive = new[] {5, 8, 14};
    private int[] aoeMaskNegative = new[] {1, 6, 15, 17, 12, 3};
    public static int PinkUpkeep = 2;


    public Pink()
    {
        Upkeep = PinkUpkeep;
        AreaOfInfluence = 2;
    }

    public Pink(Tile[,] grid, Vector3 position)
    {
        Upkeep = PinkUpkeep;
        AreaOfInfluence = 2;
        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        foreach (int index in aoeMaskPositive)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            grid[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[2] = true;
        }
        foreach (int index in aoeMaskNegative)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            grid[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[3] = true;
        }


    }

    public override void Destroy(Tile[,] grid, Vector3 position)
    {
        //revert process of building a pine
        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        foreach (int index in aoeMaskPositive)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            grid[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[2] = false;
        }
        foreach (int index in aoeMaskNegative)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            grid[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[3] = false;
        }
    }
}
