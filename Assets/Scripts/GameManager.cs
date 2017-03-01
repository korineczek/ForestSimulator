﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HexGrid))]
[RequireComponent(typeof(GameRenderer))]
public class GameManager : MonoBehaviour
{
    private WaitForSeconds interval;
    private float intervalLength = 2f;
    //Internal clock represents the amount of time that has passed since the start of the game.
    private int InternalClock = 0;
    private HexGrid grid;
    private GameRenderer gameRenderer;

    //Management Lists For Keeping Track of Trees
    private List<Tile> ActiveTiles = new List<Tile>(); 
    private List<Tile> HealthyTiles = new List<Tile>();
    private List<Tile> DyingTiles = new List<Tile>();

    public void Start()
    {
        interval = new WaitForSeconds(intervalLength);
        grid = this.GetComponent<HexGrid>();
        gameRenderer = this.GetComponent<GameRenderer>();
        StartCoroutine(GameClock());
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
        string s = string.Format("Entering Upkeep Phase, there are {0} active tiles", ActiveTiles.Count);
        Debug.Log(s);
        //pay upkeep and move trees to the right lists
        foreach (Tile ActiveTile in ActiveTiles.ToArray())
        {
            if (ActiveTile.PlacedTree.Upkeep <= ActiveTile.Resource)
            {
                //tree has enough energy to sustain itself, move to heatly trees
                gameRenderer.ChangeState(ActiveTile);
                HealthyTiles.Add(ActiveTile);
                ActiveTiles.RemoveAt(0);
            }
            else
            {
                //tree doesnt have enough energy to sustain itself - move to dying
                DyingTiles.Add(ActiveTile);
                ActiveTiles.RemoveAt(0);
            }  
        }
        s = string.Format("Upkeep phase finished, there are {0} healthy and {1} dying tiles.",HealthyTiles.Count, DyingTiles.Count);
        Debug.Log(s);
    }

    private void ExecutionPhase()
    {
        //grow trees
        //kill trees
        string s = string.Format("Killing trees, there are {0} dying tiles", DyingTiles.Count);
        Debug.Log(s);
        foreach (Tile dyingTile in DyingTiles.ToArray())
        {
            //add some resource to the ground for debug purposes
            dyingTile.Resource += 5;
            dyingTile.IsActive = false;
            DyingTiles.RemoveAt(0);
        }
        //execute other events
    }

    private void CleanupPhase()
    {
        string s = string.Format("Entering Cleanup Phase, there are {0} healthy and {1} dying tiles.", HealthyTiles.Count, DyingTiles.Count);
        Debug.Log(s);
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
        s = string.Format("Cleanup phase finished, there are {0} active tiles for next turn.", ActiveTiles.Count);
        Debug.Log(s);
    }

    public IEnumerator GameClock()
    {
        while (true)
        {
            InternalClock++;
            Debug.Log("Turn " + InternalClock);
            yield return interval;
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
