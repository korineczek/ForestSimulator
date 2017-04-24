using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using NUnit.Framework.Internal;
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

    //LEVEL TWO
    //PLANT ONE OF EACH TYPE
    public IEnumerator LevelTwo()
    {
        Debug.Log("Starting Level 2");
        while (GameStats.PlantedPines < 1 || GameStats.PlantedLeaves < 1 || GameStats.PlantedPinks < 1)
        {
            //just wait
            yield return new WaitForSeconds(1f);
        }
        //spawn victory screen and move on
        Debug.Log("Level 2 finished, planted trees:" + GameStats.PlantedTrees);
        InitiateSceneSwitch();
        yield break;

    }

    public IEnumerator LevelThree()
    {
        Debug.Log("Starting Level 3");
        int treesPlantedObjective = 1;
        int treesDeadObjective = 1;
        while (GameStats.DeadTrees < treesDeadObjective || GameStats.PlantedTrees < treesPlantedObjective)
        {
            //just wait
            yield return new WaitForSeconds(1f);
        }
        //spawn victory screen and move on
        Debug.Log("Level 3 finished");
        InitiateSceneSwitch();
        yield break;
    }

    public IEnumerator LevelFour()
    {
        //Trees can spread
        //TODO: FIGURE OUT PROPER CONDITION
        Debug.Log("Starting Level 4");
        int treesSpreadObjective = 1;
        while (GameStats.SpreadTrees < treesSpreadObjective )
        {
            //just wait
            yield return new WaitForSeconds(1f);
        }
        InitiateSceneSwitch();
        yield break;
    }

    public IEnumerator LevelFive()
    {
        //TODO: FIGURE OUT THE PROPER
        InitiateSceneSwitch();
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
            case 1:
                StartCoroutine(LevelTwo());
                break;
            case 2:
                StartCoroutine(LevelThree());
                break;
            case 3:
                StartCoroutine(LevelFour());
                break;
            case 4:
                StartCoroutine(LevelFive());
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
