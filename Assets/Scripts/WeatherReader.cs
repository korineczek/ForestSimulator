using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

public class WeatherReader : MonoBehaviour
{

    private WeatherForecast weatherForecast;
    private int lastWeatherSwitch = 0;

    public AudioSource RainPlayer;
    public AudioSource WindPlayer;
    public AudioSource SunnyPlayer;

    void Awake()
    {
        weatherForecast = (WeatherForecast)WeatherForecast.CreateInstance(typeof (WeatherForecast));
        //for debug purposes create a dummy object to test on
        weatherForecast.Forecast = new List<WeatherState>() { WeatherState.HeavyWind, WeatherState.SunClouds, WeatherState.Raining, WeatherState.Sunny };

    }
    
    void SetWeatherSounds(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.HeavyWind:
                if (SunnyPlayer.isPlaying)
                {
                    //fade out
                    StartCoroutine("FadeOutSound", SunnyPlayer);
                }
                if (RainPlayer.isPlaying) //sound to not play
                {
                    StartCoroutine("FadeOutSound", RainPlayer);
                }
                if (!WindPlayer.isPlaying) //sound to play
                {
                    WindPlayer.Play();
                }
                break;
            case WeatherState.SunClouds:
                if (WindPlayer.isPlaying)
                {
                    //fade out
                    StartCoroutine("FadeOutSound", WindPlayer);
                }
                if (RainPlayer.isPlaying) //sound to not play
                {
                    StartCoroutine("FadeOutSound", RainPlayer);
                }
                if (!SunnyPlayer.isPlaying) //sound to play
                {
                    SunnyPlayer.Play();
                }
                break;
            case WeatherState.Sunny:
                if (WindPlayer.isPlaying)
                {
                    //fade out
                    StartCoroutine("FadeOutSound", WindPlayer);
                }
                if (RainPlayer.isPlaying) //sound to not play
                {
                    StartCoroutine("FadeOutSound", RainPlayer);
                }
                if (!SunnyPlayer.isPlaying) //sound to play
                {
                    SunnyPlayer.Play();
                }
                break;
            case WeatherState.Raining:
                if (WindPlayer.isPlaying)
                {
                    //fade out
                    StartCoroutine("FadeOutSound", WindPlayer);
                }
                if (SunnyPlayer.isPlaying) //sound to not play
                {
                    StartCoroutine("FadeOutSound", SunnyPlayer);
                }
                if (!RainPlayer.isPlaying) //sound to play
                {
                    RainPlayer.Play();
                }
                break;
            case WeatherState.Earthquake:
                if (SunnyPlayer.isPlaying)
                {
                    //fade out
                    StartCoroutine("FadeOutSound", SunnyPlayer);
                }
                if (RainPlayer.isPlaying) //sound to not play
                {
                    StartCoroutine("FadeOutSound", RainPlayer);
                }
                if (!WindPlayer.isPlaying) //sound to play
                {
                    WindPlayer.Play();
                }
                break;
            case WeatherState.Overcast:
                if (WindPlayer.isPlaying)
                {
                    //fade out
                    StartCoroutine("FadeOutSound", WindPlayer);
                }
                if (SunnyPlayer.isPlaying) //sound to not play
                {
                    StartCoroutine("FadeOutSound", SunnyPlayer);
                }
                if (!RainPlayer.isPlaying) //sound to play
                {
                    RainPlayer.Play();
                }
                break;

        }
    }

    IEnumerator FadeInSound(AudioSource player)
    {
        float s = 1f;//fadein time
        float startVolume = player.volume;
        player.Play();
        for(float t=startVolume; t<1f; t+= Time.deltaTime / s)
        {
            player.volume = t;
            yield return null;
        }
    }

    IEnumerator FadeOutSound(AudioSource player)
    {
        float s = 1f;//fadeout time
        float startVolume = player.volume;
        for(float t=startVolume; t>0f; t -= Time.deltaTime/s)
        {
            player.volume = t;
            yield return null;
        }
        player.Stop();
    }

    void Update()
    {
        //block weather switching until later levels
        if (BoardData.CURRENTBOARD > 3)
        {
            //check timing for switching weathers
            if (GameStats.Turn >= lastWeatherSwitch + GameStats.WeatherInterval)
            {
                lastWeatherSwitch = GameStats.Turn;
                ChangeWeather(GameStats.WeatherIndex);
            }
            Debug.Log(GameStats.CurrentWeather);
        }
        //check if current weather is different
        SetWeatherSounds(GameStats.CurrentWeather);

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
