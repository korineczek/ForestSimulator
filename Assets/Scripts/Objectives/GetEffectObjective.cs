using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEffectObjective : Objective
{

    private bool done;

    public override bool EvaluateObjective()
    {
        Progress = string.Format("Get a Pine and an Oak to influence each other: {0}", done.ToString());
        if (GameStats.PineOakBuff > 0)
        {
            done = true;
            return true;
        }
        else
        {
            done = false;
            return false;
        }
    }

    public override bool IsWinnable()
    {
        if (GameStats.AvailablePines > 0 || GameStats.AvailableLeaves > 0 || GameStats.GrowingTrees > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
