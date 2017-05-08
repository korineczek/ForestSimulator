using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScreen : ClickHandler
{
    private Transform settingsObject;

	// Use this for initialization
	void Start ()
	{

	    settingsObject = transform.FindChild("SettingsScreen");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnRightSingleClick(GameObject pressed)
    {
        DisplaySettingsScreen();
    }

    public void DisplaySettingsScreen()
    {
        if (settingsObject.gameObject.activeSelf)
        {
            settingsObject.gameObject.SetActive(false);
        }
        else
        {
            settingsObject.gameObject.SetActive(true);
        }
    }
}
