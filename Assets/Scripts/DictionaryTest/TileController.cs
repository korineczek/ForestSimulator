using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour {
    
    public void UpdateInfo(Tile tile)
    {
        this.GetComponent<Renderer>().material.color = new Color(1 - ((tile.Resource * 20f) / 255f), 1, 1 - ((tile.Resource * 20f) / 255f));
        this.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = tile.OffsetCoordinates + "\n" + tile.Resource;
    }
}
