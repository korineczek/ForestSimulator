using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

public class WeatherReader : MonoBehaviour
{

    private WeatherForecast weatherForecast;
    private int lastWeatherSwitch = 0;

    void Awake()
    {
        weatherForecast = (WeatherForecast)WeatherForecast.CreateInstance(typeof (WeatherForecast));
        //for debug purposes create a dummy object to test on
        weatherForecast.Forecast = new List<WeatherState>() { WeatherState.Sunny, WeatherState.SunClouds, WeatherState.Raining, WeatherState.Earthquake };
    }

    void Update()
    {
        //check timing for switching weathers
        if (GameStats.Turn >= lastWeatherSwitch + GameStats.WeatherInterval)
        {
            lastWeatherSwitch = GameStats.Turn; 
            ChangeWeather(GameStats.WeatherIndex);
        }
        //Debug.Log(GameStats.CurrentWeather);
    }

    public void ChangeWeather(int index)
    {
        if (index < weatherForecast.Forecast.Count - 1)
        {
            GameStats.CurrentWeather = weatherForecast.Forecast[index + 1];
            GameStats.WeatherIndex = index + 1;
        }
            //reset weather to the beginning if the weather would
        else
        {
            GameStats.CurrentWeather = weatherForecast.Forecast[0];
            GameStats.WeatherIndex = 0;
        }
    }

}
