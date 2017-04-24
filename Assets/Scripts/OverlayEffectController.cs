using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;

public class OverlayEffectController : MonoBehaviour
{
    private Animator targetAnimator;

    private void Awake()
    {
        switch (GameObject.Find("GameManager").GetComponent<GameManager>().GameType)
        {
            case 0:
                targetAnimator = transform.GetComponent<Animator>();
                break;
            case 1:
                transform.FindChild("ParticlesOnScreen").gameObject.SetActive(true);
                targetAnimator = transform.FindChild("ParticlesOnScreen").GetComponent<Animator>();
                break;
            case 2:
                transform.FindChild("LightOnScreen").gameObject.SetActive(true);
                targetAnimator = transform.FindChild("LightOnScreen").GetComponent<Animator>();
                break;
        }
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameStats.CurrentWeather == WeatherState.SunClouds || GameStats.CurrentWeather == WeatherState.Sunny || GameStats.CurrentWeather == WeatherState.Overcast)
	    {
	        targetAnimator.SetBool("fastwind",false);
	    }
	    else
	    {
	        targetAnimator.SetBool("fastwind",true);
	    }
	}
}
