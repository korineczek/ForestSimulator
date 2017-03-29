using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;
using Tree = UnityEngine.Tree;

[RequireComponent(typeof(GameManager))]
public class GameController : MonoBehaviour
{
    private float selectionSensitivity = 5f;
    private Vector2 offsetPos;
    private Vector2 offsetPosClamped;
    public Vector3 CubePos;
    private GameManager manager;
    private List<Vector3> positionList; 

    // Use this for initialization
    void Start()
    {
        manager = this.GetComponent<GameManager>();
    }

    public void ShowTreeMenu(Vector3 worldPos)
    {
        RectTransform treeMenu = this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        offsetPos = HexCoords.World2Offset(worldPos);
        treeMenu.gameObject.SetActive(true);

        if (GameStats.UIstate == UIStatus.AcornPlanting)
        {
            Debug.Log("hiding UI");
            ShowViableArea(CubePos, false);
        }
    }

    public void PlantButton(int treeIndex)
    {
        Debug.Log(offsetPos);
        GameStats.PlantedTrees++;
        switch (treeIndex)
        {
            case 0:
                manager.PlantTree(new Pine(HexCoords.Offset2Cube(offsetPos)), offsetPos);
                break;
            case 1:
                manager.PlantTree(new Leaf(HexCoords.Offset2Cube(offsetPos)), offsetPos);
                break;
            case 2:
                manager.PlantTree(new Pink(HexCoords.Offset2Cube(offsetPos)), offsetPos);
                break;
        }
        this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(false);
        GameStats.UIstate = UIStatus.None;
    }

    public void ShowViableArea(Vector3 position, bool show)
    {
        GameStats.UIstate = UIStatus.AcornPlanting;
        CubePos = position;
        positionList = HexCoords.HexRange(CubePos, 1);
        foreach (Vector3 availableTile in positionList)
        {
            Vector2 offset = HexCoords.Cube2Offset(availableTile);
            BoardData.Map[(int)offset.x,(int)offset.y].Controller.AvailableToggle(show);
        }
    }
}