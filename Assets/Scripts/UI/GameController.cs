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
    public bool inMenu = false;
    private Vector2 offsetPos;
    private Vector2 offsetPosClamped;
    private Vector3 cubePos;
    private GameManager manager;

    // Use this for initialization
    void Start()
    {
        manager = this.GetComponent<GameManager>();
    }

    public void ShowTreeMenu(Vector3 worldPos)
    {
        GameStats.UIstate = UIStatus.TreePlanting;
        RectTransform treeMenu = this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        offsetPos = HexCoords.World2Offset(worldPos);
        treeMenu.gameObject.SetActive(true);
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
}