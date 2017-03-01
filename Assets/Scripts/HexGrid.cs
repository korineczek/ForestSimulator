using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class HexGrid : MonoBehaviour
{

    private Transform tile;
    public Transform[,] HexesTransforms;
    public Tile[,] TileArray { get; set; }

    private const int gridSize = 15;
    private float perlinScale = 5f;

    private void Awake()
    {
        //fetch prefab from resources
        tile = (Transform)Resources.Load("Prefabs/Hex", typeof(Transform));
        //initialize arrays
        HexesTransforms = new Transform[gridSize, gridSize];
        TileArray = new Tile[gridSize, gridSize];
    }

    // Use this for initialization
    void Start()
    {

        //InstantiateGrid();
        LoadLevel();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point + "   " + HexCoords.World2Offset(hit.point));
                //testplant tree
                this.GetComponent<GameManager>().PlantTree(new Pine(), HexCoords.World2Offset(hit.point));
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLevel();
        }
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
                HexesTransforms[i, j] = hex;
                TileArray[i, j] = new Tile(i, j, randomHeight);

                //set tile debug text
                HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + TileArray[i, j].CubeCoordinates;

            }
        }
        
        Level xmlLevel = new Level(TileArray);
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

    private void SaveLevel()
    {
        Level xmlLevel = new Level(TileArray);
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
        File.WriteAllText(Application.dataPath+"\\mapXMLdebug.xml",xml);
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
            //Debug.Log(tile.CubeCoordinates);
        }

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Transform hex = Instantiate(tile);
                hex.position = loadedLevel.Map[i*gridSize + j].WorldCoordinates;
                TileArray[i, j] = loadedLevel.Map[i*gridSize + j];
                //TODO: REMOVE DEBUG
                //TileArray[i,j].PlacedTree = new Pine();
                HexesTransforms[i, j] = hex;
                HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + TileArray[i, j].CubeCoordinates;
            }
        }
    }

}
