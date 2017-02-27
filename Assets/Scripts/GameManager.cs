using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private WaitForSeconds interval;
    private float intervalLength = 1f;
    //Internal clock represents the amount of time that has passed since the start of the game.
    private int InternalClock = 0;

    public void Start()
    {
        interval = new WaitForSeconds(intervalLength);
        StartCoroutine(GameClock());
    }

    public void GameStart()
    {

    }

    public void GamePause()
    {

    }

    public void GameResume()
    {

    }

    public void GameStop()
    {

    }

    public IEnumerator GameClock()
    {
        while (true)
        {
            InternalClock++;
            Debug.Log("tick" + InternalClock);
            yield return interval;
        }
    }

}
