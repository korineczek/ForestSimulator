using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;

public class Pink : Tree
{
    private int[] aoeMaskPositive = new[] {5, 8, 14};
    private int[] aoeMaskNegative = new[] {1, 6, 9, 15, 17, 12, 3};
    public static int PinkUpkeep = 8;


    public Pink()
    {
        Upkeep = PinkUpkeep;
        AreaOfInfluence = 2;
        Score = 200;
    }

    public Pink(Vector3 position)
    {
        Upkeep = PinkUpkeep;
        AreaOfInfluence = 2;
        Score = 200;
        TimePlanted = GameStats.Turn;
        TimeToGrow = 3;
        OxygenInterval = 6;

        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        foreach (int index in aoeMaskPositive)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int)offsetCoord.y > 0 &&
                (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
            {
                BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[2]++;
            }
        }
        foreach (int index in aoeMaskNegative)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int)offsetCoord.y > 0 &&
                (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
            {
                BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[3]++;
            }
        }


    }

    public override void Plant(Vector3 position, bool isManual) 
    {
        Vector2 offset = HexCoords.Cube2Offset(position);
        BoardData.Map[(int)offset.x, (int)offset.y].PlacedTree = new Pink(position);
        BoardData.Map[(int)offset.x, (int)offset.y].IsActive = true;
        PlantedManually = isManual;
    }

    public override void Destroy(Vector3 position)
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
            if ((int) offsetCoord.x > 0 && (int) offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int) offsetCoord.y > 0 &&
                (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
            {
                BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[2]--;
            }
        }
        foreach (int index in aoeMaskNegative)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int)offsetCoord.y > 0 &&
                (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
            {
                BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[3]--;
            }
        }
    }
}
