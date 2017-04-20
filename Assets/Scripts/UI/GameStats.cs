using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

namespace ForestSimulator
{
    public enum UIStatus { None, TreePlanting, AcornPlanting }
    public enum AnimState { Idle, Dying, Wind, Dead,
        Alive
    }
}



static class GameStats
{
    //GameType
    public static int GameType = 0;
    //General Stats
    public static int Score;
    public static int Turn;
    public static int Oxygen = 0;
    public static int PlantedTrees = 0;
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
        PlantedTrees = 0;
        WeatherIndex = 0;
        UIstate = UIStatus.None;
    }
}
