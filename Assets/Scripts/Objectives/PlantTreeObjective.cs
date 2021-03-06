﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTreeObjective : Objective
{

    public int TargetAmount;
    public Type TargetType;
    private string typeDesc;
    public bool IsCompleted = false; //TODO: I THINK YOU CAN DELETE THIS LINE

    public PlantTreeObjective(int amount, Type type)
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
        else if (TargetType == typeof (Tree))
        {
            currentAmount = GameStats.PlantedTrees;
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

    public override bool IsWinnable()
    {
        //determine type to check
        int available = 0;
        int inProgress = GameStats.GrowingTrees;
        if (TargetType == typeof(Pine))
        {
            available = GameStats.AvailablePines;
        }
        else if (TargetType == typeof(Leaf))
        {
            available = GameStats.AvailableLeaves;
        }
        else if (TargetType == typeof(Pink))
        {
            available = GameStats.AvailablePinks;
        }
        else if (TargetType == typeof(Tree))
        {
            available = GameStats.AvailablePines + GameStats.AvailableLeaves + GameStats.AvailablePinks;
        }

        if (available + inProgress > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
