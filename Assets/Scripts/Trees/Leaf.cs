using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Tree
{
    private int[] aoeMask = new[] { 0, 1, 2, 4, 5, 6 };
    public static int LeafUpkeep = 2;


    public Leaf()
    {
        Upkeep = LeafUpkeep;
        AreaOfInfluence = 1;
    }

    public Leaf(Tile[,] grid, Vector3 position)
    {
        Upkeep = 5;
        AreaOfInfluence = 1;
        List<Vector3> areaOfInfluence = HexCoords.HexRange(position, AreaOfInfluence);
        Vector2 offsetCoord;
        foreach (int index in aoeMask)
        {
            //convert cube coord into offset coord
            offsetCoord = HexCoords.Cube2Offset(areaOfInfluence[index]);
            //throw out a buff
            //TODO: DO THE FUCKING BUFF LOL I HAVE NO IDEA HOW
            grid[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[1] = true;
        }


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
            grid[(int)offsetCoord.x, (int)offsetCoord.y].Buffs[1] = false;
        }
    }
}
