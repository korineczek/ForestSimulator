using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{

    private Transform tile;
    private Transform[,] hexesTransforms;
    private Tile[,] tileArray;

    private const int gridSize = 15;
    private float perlinScale = 5f;

    private void Awake()
    {
        //fetch prefab from resources
        tile = (Transform)Resources.Load("Prefabs/Hex", typeof(Transform));
        //initialize arrays
        hexesTransforms = new Transform[gridSize, gridSize];
        tileArray = new Tile[gridSize, gridSize];
    }

    // Use this for initialization
    void Start()
    {

        //InstantiateGrid();
        LoadLevel();

    }

    private void InstantiateGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                float randomHeight = Mathf.PerlinNoise(i / 15f, j / 15f) * perlinScale;
                Transform hex = Instantiate(tile);
                hex.position = HexCoords.Offset2World(i, randomHeight, j);
                hexesTransforms[i, j] = hex;
                tileArray[i, j] = new Tile(i, j, randomHeight);

                //set tile debug text
                hexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + tileArray[i, j].CubeCoordinates;

            }
        }
        //TODO: SERIALIZE LEVEL AS A LIST INTO XML
        Level xmlLevel = new Level(tileArray);
        XmlSerializer xsSubmit = new XmlSerializer(typeof(Level));
        string xml = "";
        using (StringWriter sww = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer,xmlLevel);
                xml = sww.ToString();
                Debug.Log(xml);
            }
        }
        File.WriteAllText(Application.dataPath+"\\mapXML.xml",xml);
    }

    private void LoadLevel()
    {
        Level loadedLevel = new Level();

        XmlSerializer deserializer = new XmlSerializer(typeof(Level));
        using (TextReader reader = new StreamReader(Application.dataPath+"\\mapXML.xml"))
        {
            object obj = deserializer.Deserialize(reader);
            loadedLevel = (Level)obj;
            reader.Close(); 
        }
        
        foreach (Tile tile in loadedLevel.Map)
        {
            Debug.Log(tile.CubeCoordinates);
        }

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Transform hex = Instantiate(tile);
                hex.position = loadedLevel.Map[i*gridSize + j].WorldCoordinates;
                tileArray[i, j] = loadedLevel.Map[i*gridSize + j];
            }
        }
    }

}
