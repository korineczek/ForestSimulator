using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToQuestionnaire()
    {
        if (GameObject.Find("Questionnaire") != null)
        {
            Debug.Log("opening questionnaire");
            QuestionnaireManager questionnaire = GameObject.Find("Questionnaire").GetComponent<QuestionnaireManager>();
            questionnaire.EnableMenu();

        }
        else if (GameObject.Find("Questionnaire(Clone)") != null)
        {
            Debug.Log("opening questionnaire");
            QuestionnaireManager questionnaire = GameObject.Find("Questionnaire(Clone)").GetComponent<QuestionnaireManager>();
            questionnaire.EnableMenu();
        }
    }
}
