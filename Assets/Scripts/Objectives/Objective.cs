using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective
{
    public string Progress;
    public abstract bool EvaluateObjective();
    public abstract bool IsWinnable();
}
