using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;
using UnityEngine.UI;
using Tree = UnityEngine.Tree;

public class TileController : MonoBehaviour
{
    private Transform pine;
    private Transform leaf;
    private Transform pink;
    private Transform acorn;

    private Animator treeAnimator;


    public void Start()
    {
        //TODO: MOVE THIS LOADING TO SOME NOT SO RETARDED PLACE
        switch (GameStats.GameType)
        {
            case 0:
                pine = (Transform)Resources.Load("Prefabs/Trees/Pine", typeof(Transform));
                leaf = (Transform)Resources.Load("Prefabs/Trees/Leaf", typeof(Transform));
                pink = (Transform)Resources.Load("Prefabs/Trees/Pink", typeof(Transform));
                break;
            case 1:
                pine = (Transform)Resources.Load("Prefabs/Trees/PineParticlesTest", typeof(Transform));
                leaf = (Transform)Resources.Load("Prefabs/Trees/LeafParticles", typeof(Transform));
                pink = (Transform)Resources.Load("Prefabs/Trees/PinkParticles", typeof(Transform));
                break;
            case 2:
                pine = (Transform)Resources.Load("Prefabs/Trees/PineLightTest", typeof(Transform));
                leaf = (Transform)Resources.Load("Prefabs/Trees/LeafLight", typeof(Transform));
                pink = (Transform)Resources.Load("Prefabs/Trees/PinkLight", typeof(Transform));
                break;
            case 3:
                pine = (Transform)Resources.Load("Prefabs/Trees/PineALL", typeof(Transform));
                leaf = (Transform)Resources.Load("Prefabs/Trees/LeafALL", typeof(Transform));
                pink = (Transform)Resources.Load("Prefabs/Trees/PinkALL", typeof(Transform));
                break;
            case 4:
                pine = (Transform)Resources.Load("Prefabs/Trees/PineNONE", typeof(Transform));
                leaf = (Transform)Resources.Load("Prefabs/Trees/LeafNONE", typeof(Transform));
                pink = (Transform)Resources.Load("Prefabs/Trees/PinkNONE", typeof(Transform));
                break;
        }

        acorn = (Transform)Resources.Load("Prefabs/Trees/Acorn", typeof(Transform));
    }

    public void UpdateInfo(Tile tile)
    {
        //TODO: Redo so that the values are not retarded
        this.GetComponent<Renderer>().material.color = BoardData.TileGradient.Evaluate((float)tile.Resource / BoardData.MaxResource);
        /*
        this.GetComponent<Renderer>().material.color = new Color(1 - ((tile.Resource * 20f) / 255f), 1, 1 - ((tile.Resource * 20f) / 255f));
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = tile.OffsetCoordinates + "\n" + tile.Resource;
    
         */
    }

    public void SelectionToggle(bool entry)
    {

        if (entry)
        {
            this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

    }

    public void AvailableToggle(bool entry)
    {

        if (entry)
        {
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        }

    }

    public void PlantMenu()
    {
        //if (GameStats.UIstate == UIStatus.AcornPlanting)
        {
            GameController controller = GameObject.Find("GameManager").GetComponent<GameController>();
            controller.ShowTreeMenu(transform.position);
        }
    }

    public void UpdateTreeModel(Tile tile)
    {
        Transform spawnedTree;
        if (tile.PlacedTree != null && tile.TreeTransform == null)
        {
            if (tile.PlacedTree.GetType() == typeof(Pine))
            {
                //spawn pine
                spawnedTree = Instantiate(pine, tile.WorldCoordinates, pine.rotation);
                tile.TreeTransform = spawnedTree;
                treeAnimator = spawnedTree.GetComponent<Animator>();
                //spawnedTree.parent = this.transform;
            }
            else if (tile.PlacedTree.GetType() == typeof(Leaf))
            {
                //spawn leaf
                spawnedTree = Instantiate(leaf, tile.WorldCoordinates, leaf.rotation);
                tile.TreeTransform = spawnedTree;
                treeAnimator = spawnedTree.GetComponent<Animator>();
                //spawnedTree.parent = this.transform;
            }
            else if (tile.PlacedTree.GetType() == typeof(Pink))
            {
                //spawn leaf
                spawnedTree = Instantiate(pink, tile.WorldCoordinates, pink.rotation);
                tile.TreeTransform = spawnedTree;
                treeAnimator = spawnedTree.GetComponent<Animator>();
                ///spawnedTree.parent = this.transform;
            }
            ScaleTree(tile, false);
        }
        //kill tree lul
        if (tile.TreeTransform != null && tile.PlacedTree == null)
        {
            Destroy(tile.TreeTransform.gameObject);
            treeAnimator = null;
        }
    }

    public void SpawnAcorn()
    {
        Instantiate(acorn, transform.position + Vector3.up * 5f, Quaternion.identity);
    }

    public void SetAnimationState(Tile tile, AnimState state)
    {
        if (GameStats.GameType != 4)
        {


            switch (state)
            {
                case AnimState.Alive:
                    treeAnimator.SetBool("dying", false);
                    break;
                case AnimState.Dying:
                    treeAnimator.SetBool("dying", true);
                    break;
                case AnimState.Wind:
                    treeAnimator.SetBool("fastwind", true);
                    break;
                case AnimState.Idle:
                    treeAnimator.SetBool("fastwind", false);
                    break;
                case AnimState.Dead:
                    treeAnimator.SetBool("dead", true);
                    break;
            }
        }
    }

    public void ScaleTree(Tile tile, bool adult)
    {
        if (tile.TreeTransform != null && tile.PlacedTree.JustPlanted)
        {
            tile.PlacedTree.JustPlanted = false;
            tile.TreeTransform.localScale = Vector3.one / tile.PlacedTree.TimeToGrow;
        }
        //if tree is growing scale to show the progress
        else if (tile.TreeTransform != null && tile.PlacedTree.TimePlanted + tile.PlacedTree.TimeToGrow >= GameStats.Turn)
        {
            //Debug.Log("TREE NOT OLD ENOUGH " + tile.PlacedTree.TimePlanted + " " + GameStats.Turn);
            int difference = (tile.PlacedTree.TimePlanted + tile.PlacedTree.TimeToGrow) - GameStats.Turn;
            float scaleMultiplier = 1 - ((float)difference / tile.PlacedTree.TimeToGrow);
            //Debug.Log(difference + " " + scaleMultiplier);
            tile.TreeTransform.localScale = Vector3.one * scaleMultiplier;
        }
        if (tile.TreeTransform != null && adult)
        {
            //Debug.Log("TREE ADULT");
            tile.TreeTransform.localScale = Vector3.one; //TODO: THIS IS A PLACEHOLDER VALUE
        }
    }
}
