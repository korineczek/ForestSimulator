using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTreeObjective : Objective
{

    public int TargetAmount;
    public Type TargetType;
    private string typeDesc;
    public bool IsCompleted = false;

    public PlantTreeObjective(int amount, Type type)
    {
        TargetAmount = amount;
        if (type == typeof (Pine))
        {
            TargetType = typeof (Pine);
            typeDesc = "Pine";
        }
        else if (type == typeof (Leaf))
        {
            TargetType = typeof (Leaf);
            typeDesc = "Oak";
        }
        else if (type == typeof(Pink))
        {
            TargetType = typeof (Pink);
            typeDesc = "Cherry";
        }
    }

    public override bool EvaluateObjective()
    {
        //determine type to check
        int currentAmount = 0;
        if (TargetType == typeof(Pine))
        {
            currentAmount = GameStats.PlantedPines;
        }
        else if (TargetType == typeof(Leaf))
        {
            currentAmount = GameStats.PlantedLeaves;
        }
        else if (TargetType == typeof(Pink))
        {
            currentAmount = GameStats.PlantedPinks;
        }

        Progress = string.Format("Plant {0} {1}: {2}/{0}", TargetAmount, typeDesc, currentAmount);

        if (currentAmount >= TargetAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
