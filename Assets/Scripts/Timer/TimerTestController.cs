using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
    This class is to test the functionality of the Timer script. 
    Specifically, activate, deactivate, and reset functions of the timer.
    Space - activate/deactivate the timer.
    R key - reset the timer. 
*/
public class TimerTestController : MonoBehaviour
{
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            // Debug.Log("hi");
            if (timer.isActive()) {
                timer.deactivate();
            } else {
                timer.activate();
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            timer.resetTimer();
        }
    }
}
