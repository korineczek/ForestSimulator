using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitObjective : Objective {

    public float TargetAmount;
    private float startTime;


    public TimeLimitObjective(float amount)
    {
        TargetAmount = amount;
        startTime = Time.realtimeSinceStartup;
    }

    public override bool EvaluateObjective()
    {
        float timeProgress = Time.realtimeSinceStartup - startTime;
        Progress = string.Format("Play for High-Score. Time limit {0} seconds: {1}/{0}",TargetAmount,((int)timeProgress).ToString());

        if (timeProgress < TargetAmount)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsWinnable()
    {
        return true;
    }
}
