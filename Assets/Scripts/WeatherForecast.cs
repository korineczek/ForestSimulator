using System.Collections.Generic;
using Weather;
using UnityEngine;
using System.Collections;

namespace Weather
{
    public enum WeatherState
    {
        Sunny, Overcast, Raining, HeavyWind, SunClouds, Earthquake
    }
}

[CreateAssetMenu(fileName = "WeatherForecast", menuName = "Weather", order = 1)]
public class WeatherForecast : ScriptableObject
{
    public List<WeatherState> Forecast = new List<WeatherState>();
}