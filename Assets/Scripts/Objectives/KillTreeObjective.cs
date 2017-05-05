using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTreeObjective : Objective
{

    public int TargetAmount;

    public KillTreeObjective(int amount)
    {
        TargetAmount = amount;

    }

    public override bool EvaluateObjective()
    {
        Progress = string.Format("Kill {0} trees: {1}/{0}", TargetAmount,GameStats.DeadTrees);
        if (TargetAmount >= GameStats.DeadTrees)
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
        if (GameStats.AvailablePines + GameStats.AvailableLeaves + GameStats.AvailablePinks > 0 || GameStats.GrowingTrees > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
