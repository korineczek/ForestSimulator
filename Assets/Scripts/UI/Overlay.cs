using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{

    private Text score;
    private Text turn;

    private void Start()
    {
        score = this.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        turn = this.transform.GetChild(0).GetChild(1).GetComponent<Text>();
    }

    public void UpdateCanvas()
    {
        UpdateScore();
        UpdateTurn();
    }

    public void UpdateScore()
    {
        score.text = "SCORE: " + GameStats.Score;
    }

    public void UpdateTurn()
    {
        turn.text = "TURN: " + GameStats.Turn;
    }
}
