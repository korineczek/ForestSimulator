using System.Collections;
using System.Collections.Generic;
using ForestSimulator;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{

    public void UpdateInfo(Tile tile)
    {
        this.GetComponent<Renderer>().material.color = new Color(1 - ((tile.Resource * 20f) / 255f), 1, 1 - ((tile.Resource * 20f) / 255f));
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = tile.OffsetCoordinates + "\n" + tile.Resource;
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

    public void PlantMenu()
    {
        GameObject.Find("GameManager").GetComponent<GameController>().ShowTreeMenu(transform.position);
    }
}
