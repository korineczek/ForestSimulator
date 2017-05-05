using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreeObjective : Objective
{

    public int TargetAmount;
    public Type TargetType;
    private string typeDesc;

    public GetTreeObjective(int amount, Type type)
    {
        TargetAmount = amount;
        TargetType = type;
        string value;
        if (ObjectiveData.ClassNames.TryGetValue(type, out value))
        {
            typeDesc = value;
        }
    }

    public override bool EvaluateObjective()
    {
        int currentAmount = 0;
        if (TargetType == typeof(Pine))
        {
            currentAmount = GameStats.AvailablePines;
        }
        else if (TargetType == typeof(Leaf))
        {
            currentAmount = GameStats.AvailableLeaves;
        }
        else if (TargetType == typeof(Pink))
        {
            currentAmount = GameStats.AvailablePinks;
        }
        else if (TargetType == typeof(Tree))
        {
            currentAmount = GameStats.AvailablePines + GameStats.AvailableLeaves + GameStats.AvailablePinks;
        }

        Progress = string.Format("Get {0} {1}: {2}/{0}", TargetAmount, typeDesc, currentAmount);

        if (currentAmount >= TargetAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool IsWinnable()
    {
        int currentAmount = 0;
        if (TargetType == typeof(Pine))
        {
            currentAmount = GameStats.AvailablePines;
        }
        else if (TargetType == typeof(Leaf))
        {
            currentAmount = GameStats.AvailableLeaves;
        }
        else if (TargetType == typeof(Pink))
        {
            currentAmount = GameStats.AvailablePinks;
        }
        else if (TargetType == typeof(Tree))
        {
            currentAmount = GameStats.AvailablePines + GameStats.AvailableLeaves + GameStats.AvailablePinks;
        }
        if (currentAmount > 0 || GameStats.GrowingTrees > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
