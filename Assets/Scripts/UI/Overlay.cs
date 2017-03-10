using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{

    private Text score;

    private void Start()
    {
        score = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
    }

    public void UpdateCanvas()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        score.text = "SCORE: " + GameStats.Score;
    }
}
