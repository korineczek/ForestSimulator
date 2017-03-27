using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "WeatherForecast", menuName = "Weather", order = 1)]
public class WeatherForecast : ScriptableObject
{
    public List<WeatherState> Forecast = new List<WeatherState>();
}