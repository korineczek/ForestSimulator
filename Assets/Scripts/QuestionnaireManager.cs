using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        questionnaireMenu.SetActive(false);
        //Load another scene from the scene list
        Debug.Log("Loading new scene");
        GameStats.ResetStats();
        if (BoardData.CURRENTBOARD < 4)
        {
            SceneManager.LoadScene(BoardData.CURRENTBOARD + 1);
        }
        else
        {
            Application.OpenURL("http://google.com");
        }

    }

    private void SaveLevel()
    {
        List<int> continuation = continuationDesire;
        XmlSerializer xsSubmit = new XmlSerializer(typeof(Level));
        string xml = "";
        using (StringWriter sww = new StringWriter())
        {
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, continuation);
                xml = sww.ToString();
                Debug.Log(xml);
            }
        }
        File.WriteAllText(Application.dataPath + "\\Result.xml", xml);
    }

    public void EnableMenu()
    {
        questionnaireMenu.SetActive(true);
    }
}
