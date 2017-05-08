using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

public class Pine : Tree
{
    private int[] aoeMask = new[] {0, 3, 4, 5};
    public static int PineUpkeep = 3;


    public Pine()
    {
        Score = 50;
        Upkeep = PineUpkeep;
        AreaOfInfluence = 1;
    }

    public Pine(Vector3 position)
    {
        Score = 50;
        Upkeep = PineUpkeep;
        AreaOfInfluence = 1;
        TimePlanted = GameStats.Turn;
        TimeToGrow = 2;
        IsMature = false;
        OxygenInterval = 4;

        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        
        foreach (int index in aoeMask)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int)offsetCoord.y > 0 &&
                (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
            {
                BoardData.Map[(int) offsetCoord.x, (int) offsetCoord.y].Buffs[0]++;
            }
        }

        offsetCoord = HexCoords.Cube2Offset(position);
        if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 &&
                (int)offsetCoord.y > 0 && (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
        {

            BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[0]++;
        }

    }

    public override void Plant(Vector3 position, bool isManual)
    {
        Vector2 offset = HexCoords.Cube2Offset(position);
        BoardData.Map[(int)offset.x, (int)offset.y].PlacedTree = new Pine(position);
        BoardData.Map[(int)offset.x, (int)offset.y].IsActive = true;
        PlantedManually = isManual;
    }

    public override void PlantManual()
    {
        GameStats.AvailablePines++;
    }

    public override void Destroy(Vector3 position)
    {
        //revert process of building a pine
        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        foreach (int index in aoeMask)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int)offsetCoord.y > 0 &&
                (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
            {
                BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[0]--;
            }
        }
        offsetCoord = HexCoords.Cube2Offset(position);
        if ((int)offsetCoord.x > 0 && (int)offsetCoord.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 &&
                (int)offsetCoord.y > 0 && (int)offsetCoord.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1)
        {

            BoardData.Map[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[0]--;
        }
    }
}
