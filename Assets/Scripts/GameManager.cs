using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ForestSimulator;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HexGrid))]
[RequireComponent(typeof(Overlay))]
public class GameManager : MonoBehaviour
{
    private WaitForSeconds interval;
    private float fertilityThreshold = 0.5f;
    private float intervalLength = 0.25f;
    //Internal clock represents the amount of time that has passed since the start of the game.
    private int InternalClock = 0;
    private HexGrid grid;
    private Overlay UI;
    private bool gameStarted = false;

    //Management Lists For Keeping Track of Trees
    private List<Tile> ActiveTiles = new List<Tile>();
    private List<Tile> HealthyTiles = new List<Tile>();
    private List<Tile> DyingTiles = new List<Tile>();

    public void Start()
    {
        //Load Questionnaire if not found in the level
        if (GameObject.Find("Questionnaire") == null && GameObject.Find("Questionnaire(Clone)") == null)
        {
            Debug.Log("Questionnaire not found - Instantiating");
            Instantiate(Resources.Load("Prefabs/Questionnaire"));
        }

        interval = new WaitForSeconds(intervalLength);
        grid = this.GetComponent<HexGrid>();
        UI = this.GetComponent<Overlay>();
        StartCoroutine(GameClock());
    }


    //MAIN GAME ROUTINE
    public IEnumerator GameClock()
    {
        while (true)
        {
            InternalClock++;
            GameStats.Turn = InternalClock;
            //Debug.Log("Turn " + InternalClock);
            //yield return interval;
            //Execute game phases each tick
            UpkeepPhase();
            yield return interval;
            ExecutionPhase();
            yield return interval;
            CleanupPhase();
            yield return interval;
        }
    }

    private void UpkeepPhase()
    {
        //pay upkeep and move trees to the right lists
        foreach (Tile ActiveTile in ActiveTiles.ToArray())
        {
            if (ActiveTile.PlacedTree.Upkeep < ActiveTile.Resource + ActiveTile.PlacedTree.Upkeep) //check if after paying upkeep the tree still lives
            {
                //tree has enough energy to sustain itself, move to heatly trees
                //ActiveTile.Controller.SetAnimationState(ActiveTile,AnimState.Alive);
                HealthyTiles.Add(ActiveTile);
                ActiveTiles.RemoveAt(0);
            }
            else
            {
                //tree doesnt have enough energy to sustain itself - move to dying
               // ActiveTile.Controller.SetAnimationState(ActiveTile, AnimState.Dying);
                DyingTiles.Add(ActiveTile);
                ActiveTiles.RemoveAt(0);
            }
        }
    }

    private void ExecutionPhase()
    {
        //kill trees
        KillTrees();
        //spread trees
        //tree spreading and growing mechanics here
        foreach (Tile healthyTile in HealthyTiles)
        {
            //Recover tree if under base health
            if (healthyTile.PlacedTree.Health < healthyTile.PlacedTree.BaseHealth)
            {
                healthyTile.PlacedTree.Health++;
            }

            if (!healthyTile.PlacedTree.IsMature)
            {
                //grow trees
                if (healthyTile.PlacedTree.TimePlanted + healthyTile.PlacedTree.TimeToGrow <= InternalClock) //mature a tree if it has been growing long enough
                {
                    Debug.Log(healthyTile.PlacedTree + " finished growing");
                    healthyTile.PlacedTree.IsMature = true;
                    GameStats.PlantedTrees++;
                    healthyTile.PlacedTree.LastOxygen = GameStats.Turn;
                    healthyTile.Controller.SetAnimationState(healthyTile, AnimState.Alive);
                }
            }
            else if (healthyTile.PlacedTree.IsMature) //only mature trees can spread and produce oxygen
            {
                healthyTile.Controller.SetAnimationState(healthyTile, AnimState.Alive);

                if (GameStats.CurrentWeather == WeatherState.HeavyWind)
                {
                    healthyTile.Controller.SetAnimationState(healthyTile, AnimState.Wind);
                }
                else
                {
                    healthyTile.Controller.SetAnimationState(healthyTile, AnimState.Idle);
                }

                SpreadTrees(healthyTile);
                SpawnOxygen(healthyTile);
            }
        }
        //execute other events
    }

    private void CleanupPhase()
    {
        //move all trees back into active trees
        foreach (Tile healthyTile in HealthyTiles)
        {
            ActiveTiles.Add(healthyTile);
        }
        HealthyTiles.Clear();
        foreach (Tile dyingTile in DyingTiles)
        {
            ActiveTiles.Add(dyingTile);
        }
        DyingTiles.Clear();


        GameStats.Score = 0;
        for (int i = 0; i < BoardData.Map.GetLength(0); i++)
        {
            for (int j = 0; j < BoardData.Map.GetLength(0); j++)
            {
                //calc score
                if (BoardData.Map[i, j].PlacedTree != null) { GameStats.Score += BoardData.Map[i, j].PlacedTree.Score; }
                BoardData.Map[i, j].Resource = BoardData.Map[i, j].EvaluateResource();
                
                BoardData.Map[i, j].Controller.UpdateInfo(BoardData.Map[i, j]);
                //cleanup and repaint trees
                BoardData.Map[i,j].Controller.UpdateTreeModel(BoardData.Map[i,j]);
            }
        }
        //End of update
        UI.UpdateCanvas();
    }

    private void KillTrees() //Killing trees had to be changed to a specific for loop instead of foreach because removing at 0 no longer works
    {
        Tile[] dyingTile = DyingTiles.ToArray();
        for (int i = 0; i < dyingTile.Length; i++)
        {
            dyingTile[i].Controller.SetAnimationState(dyingTile[i], AnimState.Dying);

            if (GameStats.CurrentWeather == WeatherState.HeavyWind)
            {
                dyingTile[i].Controller.SetAnimationState(dyingTile[i], AnimState.Wind);
            }
            else
            {
                dyingTile[i].Controller.SetAnimationState(dyingTile[i], AnimState.Idle);
            }
            if (dyingTile[i].PlacedTree.Health <= 0)
            {
                //Debug.Log("tree dead");
                dyingTile[i].IsActive = false;
                dyingTile[i].PlacedTree.Destroy( dyingTile[i].CubeCoordinates);
                dyingTile[i].PlacedTree = null;
                DyingTiles.RemoveAt(i);
                return;
            }
            dyingTile[i].PlacedTree.Health -= 2;
        }
    }

    public void EvaluateWeather()
    {

    }

    private void SpreadTrees(Tile healthyTile)
    {
        //chance to spread
        //ROLL CHANCE FOR SPREADING
        float baseChance = UnityEngine.Random.Range(0f, 0.75f);
        float treeHealthModifier = (float)healthyTile.Resource / healthyTile.BaseResource;
        //increases with tile healt
        //TODO: REVISE THIS SPREADING METHOD TO REDUCE RUNTIME COMPLEXITY
        if (baseChance + treeHealthModifier >= fertilityThreshold)
        {
            //Acorn - Manual tree spawn control
            //healthyTile.Controller.SpawnAcorn();
            //Success - new seed spawns
            //Determine location
            
            List<Vector3> possibleLocations = HexCoords.HexRange(healthyTile.CubeCoordinates, 1);
            List<Vector3> validLocations = new List<Vector3>();
            foreach (Vector3 possibleLocation in possibleLocations)
            {
                //determine possible locations for spreading
                Vector2 offset = HexCoords.Cube2Offset(possibleLocation);
                if ((int)offset.x > 0 && (int)offset.x < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && (int)offset.y > 0 &&
                    (int)offset.y < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1 && BoardData.Map[(int)offset.x, (int)offset.y].PlacedTree == null)
                {
                    validLocations.Add(possibleLocation);
                }
            }
            if (validLocations.Count > 0)
            {
                //If there is at least one valid location, proceed with planting a tree
                int plantIndex = UnityEngine.Random.Range(0, validLocations.Count);
                Vector2 offset = HexCoords.Cube2Offset(validLocations[plantIndex]);
                //TODO: FIGURE OUT A SMART WAY TO GET THE CORRECT TYPE OF TREE FOR PLANTING
                healthyTile.PlacedTree.Plant(validLocations[plantIndex]);
                ActiveTiles.Add(BoardData.Map[(int)offset.x, (int)offset.y]);
            }
            
        }
    }

    private void SpawnOxygen(Tile healthyTile)
    {
        //Spawn oxygen
        if (healthyTile.PlacedTree.LastOxygen + healthyTile.PlacedTree.OxygenInterval <= GameStats.Turn)
        {
            GameStats.Oxygen += 100; //TODO: TEMP OXYGEN GAIN - SET TO PICKUP LATER
        }
    }

    public void PlantTree(Tree type, Vector2 position)
    {
        //check if tile is active so we cannot plant the same tree twice
        if (!BoardData.Map[(int)position.x, (int)position.y].IsActive)
        {
            BoardData.Map[(int)position.x, (int)position.y].IsActive = true;
            BoardData.Map[(int)position.x, (int)position.y].PlacedTree = type;
            ActiveTiles.Add(BoardData.Map[(int)position.x, (int)position.y]);
        }
    }
}
