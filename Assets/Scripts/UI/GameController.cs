using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;
using Tree = UnityEngine.Tree;

[RequireComponent(typeof(GameManager))]
public class GameController : ClickHandler
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

    public void HideTreeMenu()
    {
        RectTransform treeMenu = this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        treeMenu.gameObject.SetActive(false);
    }

    public void PlantButton(int treeIndex)
    {
        Debug.Log(offsetPos);
        switch (treeIndex)
        {
            case 0:
                if (GameStats.AvailablePines > 0)
                {
                    GameStats.AvailablePines--;
                    manager.PlantTree(new Pine(HexCoords.Offset2Cube(offsetPos)), offsetPos);
                }
                break;
            case 1:
                if (GameStats.AvailableLeaves > 0)
                {
                    GameStats.AvailableLeaves--;
                    manager.PlantTree(new Leaf(HexCoords.Offset2Cube(offsetPos)), offsetPos);
                }
                break;
            case 2:
                if (GameStats.AvailablePinks > 0)
                {
                    GameStats.AvailablePinks--;
                    manager.PlantTree(new Pink(HexCoords.Offset2Cube(offsetPos)), offsetPos);
                }
                break;
        }
        this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(false);
        GameStats.UIstate = UIStatus.None;
    }

    public void ShowViableArea(Vector3 position, bool show)
    {
        GameStats.UIstate = UIStatus.AcornPlanting;
        CubePos = position;
        positionList = HexCoords.HexRange(CubePos, 2);
        foreach (Vector3 availableTile in positionList)
        {
            //TODO: MARK TILES VIABLE FOR PLANTING
            Vector2 offset = HexCoords.Cube2Offset(availableTile);
            BoardData.Map[(int)offset.x,(int)offset.y].Controller.AvailableToggle(show);
        }
    }

    //Control Overrides

    public override void OnRightSingleClick(GameObject pressed)
    {
        HideTreeMenu();
    }
}