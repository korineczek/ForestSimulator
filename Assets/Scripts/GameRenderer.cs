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
    private Transform pine, leaf, pink;

    private void Start()
    {
        grid = this.GetComponent<HexGrid>();
        manager = this.GetComponent<GameManager>();
        defaultMat = (Material)Resources.Load("Debug/Default", typeof(Material));
        aliveMat = (Material)Resources.Load("Debug/Alive", typeof(Material));
        deadMat = (Material)Resources.Load("Debug/Dead", typeof(Material));
        pine = (Transform)Resources.Load("Prefabs/Trees/Pine", typeof(Transform));
        leaf = (Transform)Resources.Load("Prefabs/Trees/Leaf", typeof(Transform));
        pink = (Transform)Resources.Load("Prefabs/Trees/Pink", typeof(Transform));
    }

    public void UpdateTreeModel(Tile tile) //TODO: UGLY AS HELL CODE PLEASE KILL IT WITH FIRE LATER
    {
            Transform spawnedTree;

            if (tile.PlacedTree != null && tile.PlacedTree.GetType() == typeof(Pine) && grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] == null)
            {
                //spawn pine
                spawnedTree = Instantiate(pine, tile.WorldCoordinates, pine.rotation);
                grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] = spawnedTree;
            }
            else if (tile.PlacedTree != null && tile.PlacedTree.GetType() == typeof(Leaf) && grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] == null)
            {
                //spawn leaf
                spawnedTree = Instantiate(leaf, tile.WorldCoordinates, leaf.rotation);
                grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] = spawnedTree;
            }
            else if (tile.PlacedTree != null && tile.PlacedTree.GetType() == typeof(Pink) && grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] == null)
            {
                //spawn leaf
                spawnedTree = Instantiate(pink, tile.WorldCoordinates, pink.rotation);
                grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] = spawnedTree;
            }
            //kill tree lul
            if (grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y] != null && tile.PlacedTree == null)
            {
                Destroy(grid.TreeTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y].gameObject);
            }
    }

    public void ChangeState(Tile tile)
    {
        if (tile.TileState == Tile.State.Healthy)
        {
            grid.HexesTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y].GetComponent<Renderer>()
                .material = aliveMat;
        }
        else if (tile.TileState == Tile.State.Dying)
        {
            grid.HexesTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y].GetComponent<Renderer>()
                .material = deadMat;
        }
        else
        {
            grid.HexesTransforms[(int)tile.OffsetCoordinates.x, (int)tile.OffsetCoordinates.y].GetComponent<Renderer>()
                   .material = defaultMat;
        }
    }


}
