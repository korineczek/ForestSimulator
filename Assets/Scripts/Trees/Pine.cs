using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Pine(Tile[,] grid, Vector3 position)
    {
        Score = 50;
        Upkeep = PineUpkeep;
        AreaOfInfluence = 1;
        TimePlanted = GameStats.Turn;
        TimeToGrow = 2;
        IsMature = false;

        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        
        foreach (int index in aoeMask)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            if ((int) offsetCoord.x > 0 && (int) offsetCoord.x < 14 && (int) offsetCoord.y > 0 &&
                (int) offsetCoord.y < 14)
            {
                grid[(int) offsetCoord.x, (int) offsetCoord.y].Buffs[0]++;
            }
        }
        

    }

    public override void Plant(Tile[,] grid, Vector3 position)
    {
        Vector2 offset = HexCoords.Cube2Offset(position);
        grid[(int) offset.x, (int) offset.y].PlacedTree = new Pine(grid, position);
        grid[(int) offset.x, (int) offset.y].IsActive = true;
    }

    public override void Destroy(Tile[,] grid, Vector3 position)
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
            if ((int) offsetCoord.x > 0 && (int) offsetCoord.x < 14 && (int) offsetCoord.y > 0 &&
                (int) offsetCoord.y < 14)
            {
                grid[(int) offsetCoord.x, (int) offsetCoord.y].Buffs[0]--;
            }
        }
    }
}
