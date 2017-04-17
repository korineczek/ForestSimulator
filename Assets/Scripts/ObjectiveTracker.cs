using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartNewObjective();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(GameStats.PlantedTrees);

	}

    //LEVEL ONE
    //Plant tree and watch it grow.
    public IEnumerator LevelOne()
    {
        Debug.Log("Starting Level 1");
        int treesPlantedObjective = 1;
        while (GameStats.PlantedTrees < treesPlantedObjective)
        {
            //just wait
            yield return new WaitForSeconds(1f);
        }
        //spawn victory screen and move on
        Debug.Log("Level 1 finished, planted trees:" + GameStats.PlantedTrees);
        InitiateSceneSwitch();
        yield break;
    }

    public IEnumerator LevelTwo()
    {
        //plant all different trees
        yield break;

    }

    public IEnumerator LevelThree()
    {
        //Plant trees, some tiles are bad
        yield break;
    }

    public IEnumerator LevelFour()
    {
        //Trees can spread
        yield break;
    }

    public IEnumerator LevelFive()
    {
        yield break;
    }

    public void StartNewObjective()
    {
        //Clear all possible previous objectives before starting a new one
        StopAllCoroutines();
        switch (BoardData.CURRENTBOARD)
        {
            case 0:
                StartCoroutine(LevelOne());
                break;
        }
    }

    public void InitiateSceneSwitch()
    {
        Debug.Log("Trying to switch scene");
        //end current objective when menu opens
        StopAllCoroutines();
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
