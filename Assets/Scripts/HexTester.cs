using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexTester : MonoBehaviour
{

    public Transform DebugCube;
    private Transform[,] hexesTransforms;
    private Tile[,] tileArray;

    private const int gridSize = 15;
    private float perlinScale = 5f;

	// Use this for initialization
	void Start ()
	{
        /*
        hexesTransforms = new Transform[gridSize,gridSize];
        tileArray = new Tile[gridSize , gridSize];

	    for (int i = 0; i < gridSize; i++)
	    {
	        for (int j = 0; j < gridSize; j++)
	        {          
	            float randomHeight = Mathf.PerlinNoise(i/15f,j/15f)*perlinScale;
	            Transform hex = Instantiate(DebugCube);
	            hex.position = HexCoords.Offset2World(i, randomHeight, j);
	            hexesTransforms[i,j] = hex;
                tileArray[i,j] = new Tile(i,j, randomHeight);

                //set tile debug text
	            hexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + tileArray[i,j].CubeCoordinates;
                
	        }
	    }
        List<Vector3> testRange = HexCoords.HexRange(HexCoords.Offset2Cube(4,4), 3);
	    foreach (Vector3 hex in testRange)
	    {
	        Debug.Log(hex);
	        Vector2 offsetHex = HexCoords.Cube2Offset(hex);
	        hexesTransforms[(int) offsetHex.x, (int) offsetHex.y].GetComponent<Renderer>().material.color = Color.green;
	    }
         */
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            Debug.Log(hit.point + "   " +HexCoords.World2Offset(hit.point));
        }
        
    }
}
