using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using ForestSimulator;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionnaireManager : MonoBehaviour {

    public List<int> continuationDesire = new List<int>();
    private Slider continuationSlider;
    private GameObject questionnaireMenu;

    [Serializable]
    public struct ExportData
    {
        public string ParticipantID;
        public List<int> Results;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        continuationSlider = this.transform.GetChild(0).FindChild("ContinuationSlider").GetComponent<Slider>();
        questionnaireMenu = this.transform.GetChild(0).gameObject;
        questionnaireMenu.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddScore()
    {   
        continuationDesire.Add((int)continuationSlider.value);
        foreach (int i in continuationDesire)
        {
            Debug.Log(i);
        }
        Debug.Log(GameStats.ParticipantID);

        questionnaireMenu.SetActive(false);
        //Load another scene from the scene list
        Debug.Log("Loading new scene");
        GameStats.ResetStats();
        BoardData.CURRENTBOARD++;
        if (BoardData.CURRENTBOARD < 6)
        {
            SceneManager.LoadScene(BoardData.CURRENTBOARD + 1);
        }
        else
        {
            Application.OpenURL("https://goo.gl/forms/69NT5E1EGg19NNbJ2");
            SaveData();
            Application.Quit();
        }

    }

    private void SaveData()
    {
        ExportData data = new ExportData();
        data.ParticipantID = GameStats.ParticipantID;
        data.Results = continuationDesire;
        XmlSerializer xsSubmit = new XmlSerializer(typeof(ExportData));
        string xml = "";
        using (StringWriter sww = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, data);
                xml = sww.ToString();
                Debug.Log(xml);
            }
        }
        File.WriteAllText(Application.dataPath + "\\"+data.ParticipantID+".xml", xml);
    }

    public void EnableMenu()
    {
        questionnaireMenu.SetActive(true);
    }
}
