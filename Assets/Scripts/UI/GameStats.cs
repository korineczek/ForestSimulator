using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

namespace ForestSimulator
{
    public enum UIStatus { None, TreePlanting, AcornPlanting }
    public enum AnimState
    {
        Idle, Dying, Wind, Dead,
        Alive
    }
}



static class GameStats
{
    //GameType
    public static int GameType = 0;
    public static string ParticipantID;
    //General Stats
    public static int Score;
    public static int Turn;
    public static int Oxygen = 0;
    //Objective Tracking
    public static int GrowingTrees = 0;
    public static int PlantedTrees = 0;
    public static int PlantedPines = 0;
    public static int PlantedLeaves = 0;
    public static int PlantedPinks = 0;
    public static int DeadTrees = 0;
    public static int SpreadTrees = 0;
    public static string ObjectiveProgress;
    public static int PineOakBuff = 0;
    //Resource Tracking
    public static int AvailablePines = 5;
    public static int AvailableLeaves = 0;
    public static int AvailablePinks = 1;
    //Weather System
    public static WeatherState CurrentWeather = WeatherState.Sunny;
    public static int WeatherIndex = 0;
    public static int WeatherInterval = 5;
    //UI Related
    public static UIStatus UIstate = UIStatus.None;

    public static void ResetStats()
    {
        Score = 0;
        Turn = 0;
        Oxygen = 0;
        GrowingTrees = 0;
        PlantedTrees = 0;
        PlantedLeaves = 0;
        PlantedPines = 0;
        PlantedPinks = 0;
        DeadTrees = 0;
        SpreadTrees = 0;
        WeatherIndex = 0;
        AvailablePines = 5;
        AvailableLeaves = 5;
        AvailablePinks = 5;
        ObjectiveProgress = "";
        PineOakBuff = 0;
        UIstate = UIStatus.None;
    }

    public static void AddPlantedTree(Tree type)
    {
        PlantedTrees++;
        if (type.GetType() == typeof(Pine))
        {
            PlantedPines++;
        }
        else if (type.GetType() == typeof(Leaf))
        {
            PlantedLeaves++;
        }
        else if (type.GetType() == typeof(Pink))
        {
            PlantedPinks++;
        }
    }
}
