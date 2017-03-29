using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using ForestSimulator;

[RequireComponent(typeof(GameManager))]
public class HexGrid : MonoBehaviour
{

    private Transform tile;
    public Transform[,] TreeTransforms;

    private float perlinScale = 5f;

    private void Awake()
    {
        //fetch prefab from resources
        tile = (Transform)Resources.Load("Prefabs/Hex", typeof(Transform));
        TreeTransforms = new Transform[BoardData.BOARDSIZE, BoardData.BOARDSIZE];
    }

    // Use this for initialization
    void Start()
    {
        BoardData.GenerateBoardData(perlinScale);
        InstantiateBoard();

    }

    /// <summary>
    /// Method that generates grid based on some sort of seed value and other properties
    /// </summary>
    private void InstantiateBoard()
    {
        for (int i = 0; i < BoardData.BOARDSIZE; i++)
        {
            for (int j = 0; j < BoardData.BOARDSIZE; j++)
            {
                Transform hex = Instantiate(tile);
                hex.position = BoardData.Map[i,j].WorldCoordinates;
                BoardData.Map[i, j].Controller = hex.GetComponent<TileController>();
            }
        }
        
        //Generate slope data
        //TODO: REWORK FOR BETTER BORDER PROTECTION
        for (int i = 1; i < BoardData.BOARDSIZE - 1; i++)
        {
            for (int j = 1; j < BoardData.BOARDSIZE - 1; j++)
            {
                BoardData.Map[i, j].Controller.UpdateInfo(BoardData.Map[i, j]);
            }
        }
        #region savegame
        //save generated level to xml for loading later
        /*
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
         */
        #endregion    
    }

    private void SaveLevel()
    {
        Level xmlLevel = new Level(BoardData.Map);
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

        XmlSerializer deserializer = new XmlSerializer(typeof (Level));
        using (TextReader reader = new StreamReader(Application.dataPath + "\\mapXML.xml"))
        {
            object obj = deserializer.Deserialize(reader);
            loadedLevel = (Level) obj;
            reader.Close();
        }
    }
}
