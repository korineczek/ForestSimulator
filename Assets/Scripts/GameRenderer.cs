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
        if (tile.TileState == Tile.State.Healthy)
        {
            grid.HexesTransforms[(int) tile.OffsetCoordinates.x, (int) tile.OffsetCoordinates.y].GetComponent<Renderer>()
                .material = aliveMat;
        }
        else if (tile.TileState == Tile.State.Dying)
        {
            grid.HexesTransforms[(int) tile.OffsetCoordinates.x, (int) tile.OffsetCoordinates.y].GetComponent<Renderer>()
                .material = deadMat;
        }
        else
        {
            grid.HexesTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y].GetComponent<Renderer>()
                   .material = defaultMat;   
        }
    }


}
