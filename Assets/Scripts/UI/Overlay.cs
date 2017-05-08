using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{

    private Text score;
    private Text turn;
    private Text oxygen;
    //private Text weather;
    private Text pine;
    private Text leaf;
    private Text pink;
    private Text objectives;


    private void Start()
    {

        Transform overlayNumbersCanvas = this.transform.FindChild("OverlayCanvas").FindChild("OverlayNumbers");
        score       = overlayNumbersCanvas.FindChild("Score").GetComponent<Text>();
        oxygen      = overlayNumbersCanvas.FindChild("Oxygen").GetComponent<Text>();
        //weather     = overlayNumbersCanvas.FindChild("Weather").GetComponent<Text>();
        pine        = overlayNumbersCanvas.FindChild("PineCount").GetComponent<Text>();
        leaf        = overlayNumbersCanvas.FindChild("LeafCount").GetComponent<Text>();
        pink        = overlayNumbersCanvas.FindChild("PinkCount").GetComponent<Text>();
        objectives  = overlayNumbersCanvas.FindChild("ObjectiveTracker").GetComponent<Text>();
    }

    public void Update()
    {
        UpdateCanvas();
    }

    public void UpdateCanvas()
    {
        UpdateScore();
        UpdateOxygen();
        //UpdateWeather();
        UpdatePine();
        UpdateLeaf();
        UpdatePink();
        UpdateObjectives();

    }

    public void UpdateObjectives()
    {
        objectives.text = GameStats.ObjectiveProgress;
    }

    public void UpdateScore()
    {
        score.text = "SCORE: " + GameStats.Score;
    }

    public void UpdateTurn()
    {
        turn.text = "TURN: " + GameStats.Turn;
    }

    public void UpdateOxygen()
    {
        oxygen.text = "OXYGEN: " + GameStats.Oxygen;
    }
/*
    public void UpdateWeather()
    {
        weather.text = "WEATHER: " + GameStats.CurrentWeather;
    }
    */
    public void UpdatePine()
    {
        pine.text = GameStats.AvailablePines.ToString();
    }


    public void UpdateLeaf()
    {
        leaf.text = GameStats.AvailableLeaves.ToString();
    }

    public void UpdatePink()
    {
        pink.text = GameStats.AvailablePinks.ToString();
    }
}
