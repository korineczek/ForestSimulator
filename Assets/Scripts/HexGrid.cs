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
    public int LEVEL;
    private Transform tile;
    private float perlinScale = 5f;

    private GameObject boardContainer;

    private void Awake()
    {
        //fetch prefab from resources
        tile = (Transform)Resources.Load("Prefabs/Hex", typeof(Transform));
    }

    // Use this for initialization
    void Start()
    {
        //TODO: POSSIBLY MOVE THIS TO SOMEWHERE ELSE TO ALLOW FOR MULTIPLE LEVEL LOADING
        BoardData.CURRENTBOARD = LEVEL;
        BoardData.GenerateBoardData(perlinScale,false);
        InstantiateBoard();

    }

    /// <summary>
    /// Method that generates grid based on some sort of seed value and other properties
    /// </summary>
    private void InstantiateBoard()
    {
        boardContainer = new GameObject("Board"+BoardData.CURRENTBOARD);
        for (int i = 0; i < BoardData.BOARDSIZE[BoardData.CURRENTBOARD]; i++)
        {
            for (int j = 0; j < BoardData.BOARDSIZE[BoardData.CURRENTBOARD]; j++)
            {
                Transform hex = Instantiate(tile);
                hex.position = BoardData.Map[i,j].WorldCoordinates;
                BoardData.Map[i, j].Controller = hex.GetComponent<TileController>();
                hex.parent = boardContainer.transform;
                hex.name = i + " " + j;
            }
        }
        
        //Generate slope data
        //TODO: REWORK FOR BETTER BORDER PROTECTION
        for (int i = 1; i < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1; i++)
        {
            for (int j = 1; j < BoardData.BOARDSIZE[BoardData.CURRENTBOARD] - 1; j++)
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
