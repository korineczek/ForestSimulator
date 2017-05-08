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
        GameStats.AvailableLeaves = 3;
        GameStats.AvailablePines = 3;
        GameStats.AvailablePinks = 3;
        ObjectiveList LevelOne = new ObjectiveList(new List<Objective>(){new PlantTreeObjective(1, typeof(Tree))});
        while (LevelOne.IsCompleted == false)
        {
            //just wait
            LevelOne.EvaluateList();
            GameStats.ObjectiveProgress = LevelOne.ProgressSummary;
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
        GameStats.AvailableLeaves = 5;
        GameStats.AvailablePines = 5;
        GameStats.AvailablePinks = 5;
        Debug.Log("Starting Level 2");
        ObjectiveList LevelTwo = new ObjectiveList(new List<Objective>(){new PlantTreeObjective(1, typeof(Pine)), new PlantTreeObjective(1, typeof(Leaf)), new PlantTreeObjective(1, typeof(Pink))});
        while (LevelTwo.IsCompleted == false)
        {
            LevelTwo.EvaluateList();
            GameStats.ObjectiveProgress = LevelTwo.ProgressSummary;
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
        GameStats.AvailableLeaves = 3;
        GameStats.AvailablePines = 3;
        GameStats.AvailablePinks = 0;
        ObjectiveList LevelThree = new ObjectiveList(new List<Objective>(){new PlantTreeObjective(2, typeof(Pine)), new PlantTreeObjective(1,typeof(Leaf)),new GetEffectObjective()});
        while (LevelThree.IsCompleted == false)
        {
            LevelThree.EvaluateList();
            GameStats.ObjectiveProgress = LevelThree.ProgressSummary;
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
        GameStats.AvailableLeaves = 0;
        GameStats.AvailablePines = 10;
        GameStats.AvailablePinks = 0;
        ObjectiveList levelFour = new ObjectiveList(new List<Objective>(){new PlantTreeObjective(3,typeof(Tree)),new KillTreeObjective(1)});
        while (levelFour.IsCompleted == false)
        {
            levelFour.EvaluateList();
            GameStats.ObjectiveProgress = levelFour.ProgressSummary;
            yield return new WaitForSeconds(1f);
        }
        InitiateSceneSwitch();
        yield break;
    }

    public IEnumerator LevelFive()
    {
        Debug.Log("Starting level 5");
        GameStats.AvailableLeaves = 0;
        GameStats.AvailablePines = 3;
        GameStats.AvailablePinks = 0;
        ObjectiveList LevelFive = new ObjectiveList(new List<Objective>() {new GetTreeObjective(10, typeof (Pine))});
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
    }   
}
