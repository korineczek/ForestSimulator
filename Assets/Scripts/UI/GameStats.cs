using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weather;

static class GameStats
{
    //General Stats
    public static int Score;
    public static int Turn;
    public static int Oxygen = 0;
    public static int PlantedTrees = 0;
    //Weather System
    public static WeatherState CurrentWeather = WeatherState.Sunny;
    public static int WeatherIndex = 0;
    public static int WeatherInterval = 5;
}
