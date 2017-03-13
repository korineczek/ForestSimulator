using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HexGrid))]
[RequireComponent(typeof(GameRenderer))]
public class GameManager : MonoBehaviour
{
    private WaitForSeconds interval;
    private float fertilityThreshold = 0.5f;
    private float intervalLength = 0.25f;
    //Internal clock represents the amount of time that has passed since the start of the game.
    private int InternalClock = 0;
    private HexGrid grid;
    private GameRenderer gameRenderer;
    private Overlay UI;
    private bool gameStarted = false;

    //Management Lists For Keeping Track of Trees
    private List<Tile> ActiveTiles = new List<Tile>(); 
    private List<Tile> HealthyTiles = new List<Tile>();
    private List<Tile> DyingTiles = new List<Tile>();

    public void Start()
    {
        interval = new WaitForSeconds(intervalLength);
        grid = this.GetComponent<HexGrid>();
        gameRenderer = this.GetComponent<GameRenderer>();
        UI = this.GetComponent<Overlay>();
        //StartCoroutine(GameClock());
    }

    public void Update()
    {
        if (GameStats.PlantedTrees > 2 && gameStarted == false)
        {
            gameStarted = true;
            StartCoroutine(GameClock());
        }
    }

    public void GameStart()
    {

    }

    public void GamePause()
    {

    }

    public void GameResume()
    {

    }

    public void GameStop()
    {

    }

    public void PlantTree(Tree type, Vector2 position)
    {
        //check if tile is active so we cannot plant the same tree twice
        if (!grid.TileArray[(int) position.x, (int) position.y].IsActive)
        {
            grid.TileArray[(int) position.x, (int) position.y].IsActive = true;
            grid.TileArray[(int) position.x, (int) position.y].PlacedTree = type;
            ActiveTiles.Add(grid.TileArray[(int) position.x, (int) position.y]);
        }
    }

    private void UpkeepPhase()
    {
        //string s = string.Format("Entering Upkeep Phase, there are {0} active tiles", ActiveTiles.Count);
        //Debug.Log(s);
        //pay upkeep and move trees to the right lists
        foreach (Tile ActiveTile in ActiveTiles.ToArray())
        {
            if (ActiveTile.PlacedTree.Upkeep < ActiveTile.Resource + ActiveTile.PlacedTree.Upkeep) //check if after paying upkeep the tree still lives
            {
                //tree has enough energy to sustain itself, move to heatly trees
                ActiveTile.TileState = Tile.State.Healthy;
                //gameRenderer.ChangeState(ActiveTile);
                HealthyTiles.Add(ActiveTile);
                ActiveTiles.RemoveAt(0);
            }
            else
            {
                //tree doesnt have enough energy to sustain itself - move to dying
                ActiveTile.TileState = Tile.State.Dying;
                //gameRenderer.ChangeState(ActiveTile);
                DyingTiles.Add(ActiveTile);
                ActiveTiles.RemoveAt(0);
            }  
        }
        //s = string.Format("Upkeep phase finished, there are {0} healthy and {1} dying tiles.",HealthyTiles.Count, DyingTiles.Count);
        //Debug.Log(s);
    }

    private void ExecutionPhase()
    {
        //grow trees
        //kill trees
        //string s = string.Format("Killing trees, there are {0} dying tiles", DyingTiles.Count);
        //Debug.Log(s);
        foreach (Tile dyingTile in DyingTiles.ToArray())
        {
            //add some resource to the ground for debug purposes
            dyingTile.IsActive = false;
            //dyingTile.TileState = Tile.State.Inactive;
            //Kill trees
            dyingTile.PlacedTree.Destroy(grid.TileArray, dyingTile.CubeCoordinates);
            dyingTile.PlacedTree = null;
            //gameRenderer.ChangeState(dyingTile);
            DyingTiles.RemoveAt(0);
        }
        //spread trees
        //tree spreading mechanics here
        foreach (Tile healthyTile in HealthyTiles)
        {
            //chance to spread
            //ROLL CHANCE FOR SPREADING
            float baseChance = UnityEngine.Random.Range(0f, 0.75f);
            float treeHealthModifier = (float)healthyTile.Resource/healthyTile.BaseResource; //increases with tile healt
            //TODO: REVISE THIS SPREADING METHOD TO REDUCE RUNTIME COMPLEXITY
            if (baseChance + treeHealthModifier >= fertilityThreshold)
            {
                //Debug.Log(baseChance + treeHealthModifier);
                //Success - new seed spawns
                //Determine location
                List<Vector3> possibleLocations = HexCoords.HexRange(healthyTile.CubeCoordinates,1);
                List<Vector3> validLocations = new List<Vector3>();
                foreach (Vector3 possibleLocation in possibleLocations)
                {
                    //determine possible locations for spreading
                    Vector2 offset = HexCoords.Cube2Offset(possibleLocation);
                    //offset.x = Mathf.Clamp(offset.x, 0, 14);
                    //offset.x = Mathf.Clamp(offset.y, 0, 14);
                    if ((int) offset.x > 0 && (int) offset.x < 14 && (int) offset.y > 0 &&
                (int) offset.y < 14 && grid.TileArray[(int)offset.x, (int)offset.y].PlacedTree == null) { validLocations.Add(possibleLocation);}
                }
                if (validLocations.Count > 0)
                {
                    //If there is at least one valid location, proceed with planting a tree
                    int plantIndex = UnityEngine.Random.Range(0, validLocations.Count);
                    Vector2 offset = HexCoords.Cube2Offset(validLocations[plantIndex]);
                    //TODO: FIGURE OUT A SMART WAY TO GET THE CORRECT TYPE OF TREE FOR PLANTING
                    healthyTile.PlacedTree.Plant(grid.TileArray, validLocations[plantIndex]);
                    ActiveTiles.Add(grid.TileArray[(int)offset.x, (int)offset.y]);
                }
            }
        }

        //execute other events
    }

    private void CleanupPhase()
    {
        //string s = string.Format("Entering Cleanup Phase, there are {0} healthy and {1} dying tiles.", HealthyTiles.Count, DyingTiles.Count);
        //Debug.Log(s);
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
        //recalculate
        //TODO: CLEANUP THIS PART AFTER BASE COLORING WORKS
        //reset score to 0
        GameStats.Score = 0;
        for (int i = 0; i < grid.TileArray.GetLength(0); i++)
        {
            for (int j = 0; j < grid.TileArray.GetLength(0); j++)
            {
                //calc score
                if (grid.TileArray[i, j].PlacedTree != null) { GameStats.Score += grid.TileArray[i, j].PlacedTree.Score; }
                grid.TileArray[i, j].Resource = grid.TileArray[i, j].EvaluateResource();
                grid.HexesTransforms[i, j].GetComponent<Renderer>().material.color = new Color(1 - ((grid.TileArray[i, j].Resource * 20f) / 255f), 1, 1 - ((grid.TileArray[i, j].Resource * 20f) / 255f));
                //grid.HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + grid.TileArray[i, j].Resource;
                grid.HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = grid.TileArray[i, j].Resource.ToString();
                //cleanup and repaint trees
                gameRenderer.UpdateTreeModel(grid.TileArray[i,j]);
            }
        }

        //End of update
        UI.UpdateCanvas();
        //s = string.Format("Cleanup phase finished, there are {0} active tiles for next turn.", ActiveTiles.Count);
        //Debug.Log(s);
    }

    public IEnumerator GameClock()
    {
        while (InternalClock < 100)
        {
            InternalClock++;
            GameStats.Turn = InternalClock;
            Debug.Log("Turn " + InternalClock);
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

}
