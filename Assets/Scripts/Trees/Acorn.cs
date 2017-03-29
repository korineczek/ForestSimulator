using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForestSimulator;

public class Acorn : MonoBehaviour
{

    public void PlantAcorn()
    {
        GameController controller = GameObject.Find("GameManager").GetComponent<GameController>();
        controller.ShowViableArea(HexCoords.Offset2Cube(HexCoords.World2Offset(transform.position)),true);
    }
}
