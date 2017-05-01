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
    private Text weather;
    private Text pine;
    private Text leaf;
    private Text pink;


    private void Start()
    {
        score = this.transform.GetChild(0).GetChild(0).FindChild("Score").GetComponent<Text>();
        //turn = this.transform.GetChild(0).GetChild(0).FindChild("Score").GetComponent<Text>();
        oxygen = this.transform.GetChild(0).GetChild(0).FindChild("Oxygen").GetComponent<Text>();
        weather = this.transform.GetChild(0).GetChild(0).FindChild("Weather").GetComponent<Text>();
        pine = this.transform.GetChild(0).GetChild(0).FindChild("PineCount").GetComponent<Text>();
        leaf = this.transform.GetChild(0).GetChild(0).FindChild("LeafCount").GetComponent<Text>();
        pink = this.transform.GetChild(0).GetChild(0).FindChild("PinkCount").GetComponent<Text>();
    }

    public void Update()
    {
        UpdateCanvas();
    }

    public void UpdateCanvas()
    {
        UpdateScore();
        //UpdateTurn();
        UpdateOxygen();
        UpdateWeather();
        UpdatePine();
        UpdateLeaf();
        UpdatePink();

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

    public void UpdateWeather()
    {
        weather.text = "WEATHER: " + GameStats.CurrentWeather;
    }

    public void UpdatePine()
    {
        pine.text = "PINE COUNT " + GameStats.AvailablePines;
    }


    public void UpdateLeaf()
    {
        leaf.text = "LEAF COUNT " + GameStats.AvailableLeaves;
    }

    public void UpdatePink()
    {
        pink.text = "PINK COUNT " + GameStats.AvailablePinks;
    }
}
