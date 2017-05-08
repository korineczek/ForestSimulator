using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;

public class OverlayEffectController : MonoBehaviour
{
    private Animator targetAnimator;
    private Animator secondaryAnimator;

    private void Awake()
    {
        switch (GameStats.GameType)
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
            case 3:
                transform.FindChild("ParticlesOnScreen").gameObject.SetActive(true);
                transform.FindChild("LightOnScreen").gameObject.SetActive(true);
                targetAnimator = transform.FindChild("ParticlesOnScreen").GetComponent<Animator>();
                secondaryAnimator = transform.FindChild("LightOnScreen").GetComponent<Animator>();
                break;
            case 4:
                break;
        }
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (GameStats.GameType != 4)
	    {
	        if (GameStats.CurrentWeather == WeatherState.SunClouds || GameStats.CurrentWeather == WeatherState.Sunny ||
	            GameStats.CurrentWeather == WeatherState.Overcast)
	        {
	            targetAnimator.SetBool("fastwind", false);
	            if (secondaryAnimator != null)
	            {
	                secondaryAnimator.SetBool("fastwind", false);
	            }
	        }
	        else
	        {
	            targetAnimator.SetBool("fastwind", true);
	            if (secondaryAnimator != null)
	            {
	                secondaryAnimator.SetBool("fastwind", true);
	            }
	        }
	    }
	}
}
