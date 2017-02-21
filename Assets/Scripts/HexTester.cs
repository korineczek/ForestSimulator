using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexTester : MonoBehaviour
{

    public Transform DebugCube;
    private Transform[,] hexesTransforms;
    private Tile[,] tileArray;

	// Use this for initialization
	void Start ()
	{
        hexesTransforms = new Transform[10,10];
        tileArray = new Tile[10,10];

	    for (int i = 0; i < 10; i++)
	    {
	        for (int j = 0; j < 10; j++)
	        {
	            Transform hex = Instantiate(DebugCube);
	            hex.position = HexCoords.Offset2World(i, 0, j);
	            hexesTransforms[i,j] = hex;
                tileArray[i,j] = new Tile(i,j, 0);

                //set tile debug text
	            hexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j +"\n" + tileArray[i,j].CubeCoordinates.ToString();

	        }
	    }


	    List<Vector3> testRange = HexCoords.HexRange(HexCoords.Offset2Cube(4,4), 3);
	    foreach (Vector3 hex in testRange)
	    {
	        Debug.Log(hex);
	        Vector2 offsetHex = HexCoords.Cube2Offset(hex);
	        hexesTransforms[(int) offsetHex.x, (int) offsetHex.y].GetComponent<Renderer>().material.color = Color.green;
	    }




	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
