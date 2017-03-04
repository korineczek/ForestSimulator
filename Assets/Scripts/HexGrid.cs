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
    private float perlinScale = 10f;

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

    /// <summary>
    /// Method that generates grid based on some sort of seed value and other properties
    /// </summary>
    private void InstantiateGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                //get random height for tiles based on noise
                float randomHeight = (Mathf.PerlinNoise(i / 20f, j / 20f) * perlinScale +(UnityEngine.Random.Range(-0.5f,0.5f)/1))*1f;
                Transform hex = Instantiate(tile);
                hex.position = HexCoords.Offset2World(i, randomHeight, j);
                //assign transforms to the appropriate arrays
                HexesTransforms[i, j] = hex;
                TileArray[i, j] = new Tile(i, j, randomHeight);
                //set tile debug text
                HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + TileArray[i, j].CubeCoordinates;

            }
        }
        
        //Generate slope data
        //TODO: REWORK FOR BETTER BORDER PROTECTION
        for (int i = 1; i < gridSize-1; i++)
        {
            for (int j = 1; j < gridSize-1; j++)
            {
                TileArray[i, j].Slope = GetMaxSlope(i, j);
                //Debug.Log(TileArray[i, j].Slope);
                //TODO: NORMALIZE SLOPES SO THAT THE COLOR DOENST OVERFLOW
                TileArray[i, j].Resource = (int)(TileArray[i, j].Resource * (1-TileArray[i, j].Slope));
                //Debug.Log(TileArray[i, j].Resource * TileArray[i, j].Slope);

                HexesTransforms[i, j].GetComponent<Renderer>().material.color = new Color(1- ((TileArray[i, j].Resource*20f) / 255f), 1, 1- ((TileArray[i, j].Resource*20f) / 255f));
                HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + TileArray[i, j].Resource;
            }
        }

        //save generated level to xml for loading later
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


    /// <summary>
    /// get max slope for each tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private float GetMaxSlope(int x, int y)
    {
        /*
        //variant 1 - max slope
        float maxSlope = 0;
        float currentSlope = 0;
        currentSlope = HexCoords.HexSlope(TileArray[x+0, y-1], TileArray[x+1, y+1]);
        if (currentSlope > maxSlope) { maxSlope = currentSlope; }
        currentSlope = HexCoords.HexSlope(TileArray[x+1, y-1], TileArray[x+0, y+1]);
        if (currentSlope > maxSlope) { maxSlope = currentSlope; }
        currentSlope = HexCoords.HexSlope(TileArray[x+1, y+0], TileArray[x-1, y+0]);
        if (currentSlope > maxSlope) { maxSlope = currentSlope; }
        return maxSlope;
         */

        //variant 2 average slope
        return (HexCoords.HexSlope(TileArray[x + 0, y - 1], TileArray[x, y]) +
                              HexCoords.HexSlope(TileArray[x + 1, y - 1], TileArray[x, y]) +
                              HexCoords.HexSlope(TileArray[x + 1, y + 0], TileArray[x, y]) + HexCoords.HexSlope(TileArray[x, y], TileArray[x + 1, y + 1]) +
                              HexCoords.HexSlope(TileArray[x, y], TileArray[x + 0, y + 1]) +
                              HexCoords.HexSlope(TileArray[x, y], TileArray[x - 1, y + 0])) / 6;
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
                HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + TileArray[i, j].Resource;
            }
        }

        //colorHexes based on resource value
        for (int i = 1; i < gridSize - 1; i++)
        {
            for (int j = 1; j < gridSize - 1; j++)
            {
                HexesTransforms[i, j].GetComponent<Renderer>().material.color = new Color(1 - ((TileArray[i, j].Resource * 20f) / 255f), 1, 1 - ((TileArray[i, j].Resource * 20f) / 255f));
                HexesTransforms[i, j].GetChild(0).GetChild(0).GetComponent<Text>().text = i + " " + j + "\n" + TileArray[i, j].Resource;
            }
        }
    }

}
