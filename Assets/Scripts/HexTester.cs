using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTester : MonoBehaviour
{

    public Transform DebugCube;

	// Use this for initialization
	void Start ()
	{
	    Debug.Log(HexCoords.Offset2World(0, 0, 0));
        Debug.Log(HexCoords.Offset2World(1, 0, 0));
        Debug.Log(HexCoords.Offset2World(2, 0, 0));

        Debug.Log(HexCoords.Offset2World(0, 0, 1));
        Debug.Log(HexCoords.Offset2World(1, 0, 1));
        Debug.Log(HexCoords.Offset2World(2, 0, 1));

	    for (int i = 0; i < 7; i++)
	    {
	        for (int j = 0; j < 7; j++)
	        {
	            Transform hex = Instantiate(DebugCube);
	            hex.position = HexCoords.Offset2World(i, 0, j);
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
