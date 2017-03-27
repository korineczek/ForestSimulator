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
    private Transform pine, leaf, pink;

    private void Start()
    {
        grid = this.GetComponent<HexGrid>();
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
}
