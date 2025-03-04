using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCounter : MonoBehaviour
{
    public GameObject[] userFacingCheckpoints;
    public static GameObject[] checkpoints;
    public static int currentCheckpoint = 0;
    private bool finished = false; 
    public GameObject winTextObject;
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = userFacingCheckpoints;
        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        // if (other.gameObject.CompareTag("PickUp")) {
        //     other.gameObject.SetActive(false);
        //     count++;
        //     SetCountText();
        // }

        //if finished level, no need to check for collisions.
        if (finished) return;

        //checkpoint system - check if collision is next checkpoint and do something if not already done the level
        if (other.gameObject == checkpoints[currentCheckpoint] ) {
            Debug.Log("hit correct checkpoint number " + currentCheckpoint);
            currentCheckpoint = (currentCheckpoint + 1) % checkpoints.Length;
            
            //if we loop back to 0, we are finished.
            if (currentCheckpoint == 0) {
                Debug.Log("hit all checkpoints");
                finished = true;
                winTextObject.SetActive(true);
                Time.timeScale = 0.50f;
                //set next level buttons active here
            }
        }
    }
}
