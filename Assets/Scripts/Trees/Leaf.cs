using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Tree
{
    private int[] aoeMask = new[] { 0, 1, 2, 3, 4, 5, 6 };
    public static int LeafUpkeep = 4;


    public Leaf()
    {
        Upkeep = LeafUpkeep;
        AreaOfInfluence = 1;
        Score = 100;
    }

    public Leaf(Tile[,] grid, Vector3 position)
    {
        Score = 100;
        Upkeep = LeafUpkeep;
        AreaOfInfluence = 1;
        TimePlanted = GameStats.Turn;
        TimeToGrow = 3;
        IsMature = false;
        OxygenInterval = 2;


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
                grid[(int) offsetCoord.x, (int) offsetCoord.y].Buffs[1]++;
            }
        }

    }

    public override void Plant(Tile[,] grid, Vector3 position)
    {
        Vector2 offset = HexCoords.Cube2Offset(position);
        grid[(int)offset.x, (int)offset.y].PlacedTree = new Leaf(grid, position);
        grid[(int)offset.x, (int)offset.y].IsActive = true;
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
                grid[(int) offsetCoord.x, (int) offsetCoord.y].Buffs[1]--;
            }
        }
    }
}
