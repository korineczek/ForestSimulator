using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroLevel : MonoBehaviour
{

    private InputField inputField;

	// Use this for initialization
	void Start ()
	{
	    inputField = transform.FindChild("Interactive").FindChild("ParticipantID").GetComponent<InputField>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToExperiment()
    {
        GameStats.ParticipantID = inputField.text;
        SceneManager.LoadScene("01");
    }
}
