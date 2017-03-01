using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing functions made for visualizing the game state on the hex grid
/// </summary>
[RequireComponent(typeof(HexGrid))]
[RequireComponent(typeof(GameManager))]
public class GameRenderer : MonoBehaviour
{

    private HexGrid grid;
    private GameManager manager;
    private Material defaultMat, aliveMat, deadMat;

    private void Start()
    {
        grid = this.GetComponent<HexGrid>();
        manager = this.GetComponent<GameManager>();
        defaultMat = (Material) Resources.Load("Debug/Default",typeof(Material));
        aliveMat = (Material)Resources.Load("Debug/Alive", typeof(Material));
        deadMat = (Material)Resources.Load("Debug/Dead", typeof(Material));
    }

    public void ChangeState(Tile tile)
    {
        grid.HexesTransforms[(int) tile.OffsetCoordinates.x, (int) tile.OffsetCoordinates.y].GetComponent<Renderer>()
            .material = aliveMat;
    }


}
