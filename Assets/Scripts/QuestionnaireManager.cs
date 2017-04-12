using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void EnableMenu()
    {
        questionnaireMenu.SetActive(true);
    }
}
