using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree = UnityEngine.Tree;

[RequireComponent(typeof(GameManager))]
public class GameController : MonoBehaviour
{

    private Vector3 cubeSelection = new Vector3();
    private RectTransform selectionCanvas;
    private float selectionSensitivity = 5f;
    public bool inMenu = false;
    private Vector2 offsetPos;
    private Vector2 offsetPosClamped;
    private GameManager manager;
    private HexGrid grid;


    // Use this for initialization
    void Start()
    {
        selectionCanvas = this.transform.GetChild(1).GetComponent<RectTransform>();
        manager = this.GetComponent<GameManager>();
        grid = this.GetComponent<HexGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inMenu);
        if (!inMenu)
        {
            #region TileSelection
            //keyboard and mouse alternative
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.point + "   " + HexCoords.World2Offset(hit.point));
            }

            //controller alternative DISABLED FOR NOW. K+M ONLY
            float selX = Input.GetAxis("SelectionHorizontal");
            float selY = Input.GetAxis("SelectionVertical");
            //TODO: PROPER CLAMPING SO WE DONT GO OUT OF BOUNDS
            cubeSelection += new Vector3(selX, -selX - selY, selY) * selectionSensitivity * Time.deltaTime;
           
            
            //we have to clamp offset between the sizes of our board
            //offsetPos = HexCoords.Cube2Offset(HexCoords.RoundCubeCoord(cubeSelection));
            offsetPos = HexCoords.World2Offset(hit.point);
            offsetPosClamped = offsetPos;
            Vector3 worldPos = HexCoords.Offset2World(offsetPosClamped, 0);

            selectionCanvas.position = new Vector3(worldPos.x, 0.1f, worldPos.z);
            //Debug.Log(cubeSelection);
       

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("setting true");
            inMenu = true;
            SelectTile(offsetPosClamped);
        }
        } 
            #endregion
    }

    private void SelectTile(Vector2 offsetCoord)
    {
        ShowTreeMenu(HexCoords.Offset2World(offsetCoord, 0));
    }

    private void ShowTreeMenu(Vector3 worldPos)
    {
        RectTransform treeMenu = this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>();
        treeMenu.gameObject.SetActive(true);
        Debug.Log(Camera.main.WorldToScreenPoint(worldPos));
        Vector2 camScreenPos = Camera.main.WorldToScreenPoint(worldPos);
        //TODO: SET MENU POSITION TO CAMERA POSITION, PROBLEM WITH CONVERTING FROM CANVAS SIZE TO PIXEL SIZE
        //treeMenu.position = new Vector3(camScreenPos.x + Camera.main.pixelWidth/2f,camScreenPos.y + Camera.main.pixelHeight/2f,0);
    }

    public void PlantButton(int treeIndex)
    {
        Debug.Log(offsetPos);
        GameStats.PlantedTrees++;
        switch (treeIndex)
        {
            case 0:
                manager.PlantTree(new Pine(grid.TileArray, HexCoords.Offset2Cube((int)offsetPos.x, (int)offsetPos.y)), new Vector2(offsetPos.x, offsetPos.y));
                break;
            case 1:
                manager.PlantTree(new Leaf(grid.TileArray, HexCoords.Offset2Cube((int)offsetPos.x, (int)offsetPos.y)), new Vector2(offsetPos.x, offsetPos.y));
                break;
            case 2:
                manager.PlantTree(new Pink(grid.TileArray, HexCoords.Offset2Cube((int)offsetPos.x, (int)offsetPos.y)), new Vector2(offsetPos.x, offsetPos.y));
                break;
        }
        //Debug.Log("setting false");
        //inMenu = false;
        this.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().gameObject.SetActive(false);
        StartCoroutine(ClearInput());
    }

    /// <summary>
    /// prevents double use of confirmation button
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClearInput()
    {
        yield return new WaitForEndOfFrame();
        inMenu = false;
        yield return null;
    }
}
