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
        int treesSpreadObjective = 2;
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
        Debug.Log("Starting level 5");
        /*
        //TODO: FIGURE OUT THE PROPER
        int treesSpreadObjective = 5;
        int pinkPlantedObjective = 1;
        while (GameStats.SpreadTrees < treesSpreadObjective || GameStats.PlantedPinks < pinkPlantedObjective )
        {
            //just wait
            Debug.Log(GameStats.SpreadTrees + " " + GameStats.PlantedPinks);
            yield return new WaitForSeconds(1f);
        }
         
         */
        //TESTING NEW OBJECTIVE SYSTEM
        ObjectiveList LevelFive = new ObjectiveList(new List<Objective> {new PlantTreeObjective(1, typeof (Pine)), new PlantTreeObjective(1, typeof(Pink))});
        while (LevelFive.IsCompleted == false)
        {
            LevelFive.EvaluateList();
            GameStats.ObjectiveProgress = LevelFive.ProgressSummary;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("SHIT IT ACTUALLY WORKS");
        InitiateSceneSwitch();
        yield break;
    }

    public void StartNewObjective()
    {
        //Clear all possible previous objectives before starting a new one
        StopAllCoroutines();
        Debug.Log(BoardData.CURRENTBOARD);
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
        Transform camera = GameObject.Find("Camera").transform;
        switch (GameStats.GameType)
        {
            case 0:
                camera.FindChild("VictoryAnimation").gameObject.SetActive(true);
                break;
            case 1:
                camera.FindChild("VictoryParticles").gameObject.SetActive(true);
                break;
            case 2:
                camera.FindChild("VictoryLight").gameObject.SetActive(true);
                break;
        }
        //end current objective when menu opens
        StopAllCoroutines();
        /*
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
         */
    }

    
}
