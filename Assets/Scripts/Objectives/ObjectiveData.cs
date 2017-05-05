using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectiveData
{

    public static Dictionary<Type, string> ClassNames = new Dictionary<Type, string>()
    {
        {typeof(Pine), "Pine"},
        {typeof(Leaf), "Oak"},
        {typeof(Pink), "Cherry"},
        {typeof(Tree), "Tree"}
    };
}
