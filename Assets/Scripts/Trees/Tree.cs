using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
[XmlInclude(typeof(Pine))]
[XmlInclude(typeof(Leaf))]
[XmlInclude(typeof(Pink))]
public abstract class Tree
{
    public int Cost;
    public int Score;
    public int Health;
    public int Upkeep;
    public int AreaOfInfluence;
    public int TimePlanted;
    public int TimeToGrow;
    public bool IsMature;
    public bool IsActive;
    

    public abstract void Plant(Tile[,] grid, Vector3 position);
    public abstract void Destroy(Tile[,] grid, Vector3 position);
}
