using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
    Timer script attached to the Timer UI. 
    Allows user to set the total time of timer, activate/deactivate timer, and reset timer.

    Notes:
        - Timer needs to be deactivated when pause menu is active.
        - timer needs to be activated when pause menu is deactivated.
        - Timer needs to be reset after each level
            - is this free? i think so.
        - Timer, when depleted, needs to display lose screen. Lose screen needs to let player restart game/level.
*/
public class Timer : MonoBehaviour
{
    public float totalTime = 10.0f;
    public float timeRemainingDisplay;
    public GameObject losePanel;
    public TextMeshProUGUI timerText;

    private bool timerActive = true;
    private bool timeExceeded = false;
    private float timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = totalTime;
        displayTimeRemaining();
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        //decrement the timer
        if (!timeExceeded && timerActive) {
            timeRemaining -= Time.deltaTime;
        }

        //timer over, display loss screen and change time scale
        if (timeRemaining <= 0.0f && !timeExceeded) {
            timeExceeded = true;
            Debug.Log("timer ended");
            losePanel.SetActive(true);
            Time.timeScale = 0.50f;
        }
        
        displayTimeRemaining();
        SetTimerText();

    }

    void displayTimeRemaining() {
        timeRemainingDisplay = timeRemaining;
    }

    public bool isActive() {
        return timerActive;
    }
    public void activate() {
        timerActive = true;
    }

    public void deactivate() {
        timerActive = false;
    }

    public void resetTimer() {
        timeRemaining = totalTime;
    }

    void SetTimerText() {
        float t = Mathf.Ceil(timeRemaining);
        timerText.text = "Time: " + t.ToString();
    }
}
