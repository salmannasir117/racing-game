using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCounter : MonoBehaviour
{
    public GameObject[] userFacingCheckpoints;
    public static GameObject[] checkpoints;
    public static int currentCheckpoint;
    private bool finished = false; 
    // public GameObject winTextObject;
    public GameObject winPanel;
    public Timer timer;
    // Start is called before the first frame update

    //Audio to play once entering correct checkpoint
    public AudioSource correctSoundAudioSource;
    //Audio to play once entering wrong checkpoint
    public AudioSource warningSoundAudioSource;

    /* Variables to help with animating current checkpoint */
    private Material originalMaterial;      //original material, used to reset look after checkpoint passed
    private Material newMaterial;           //new material that will be animated

    private float totalTime;                //total time the game has been running (seconds)
    private float animationLength = 1.5f;   //how long animation is (seconds)

    //Animation will smoothly translate from startColor to endColor and then back in animationLength seconds
    private Color startColor = new Color(46, 197, 6) / 255.0f;      //dark green
    private Color endColor = new Color(74, 255, 0) / 255.0f;        //light green

    //Get both pillars and change their material to mat
    //Author's Note: if the design of the checkpoint prefab changes, this has to change to get different children
    private void setCurrentCheckpointMaterial(Material mat) {
        checkpoints[currentCheckpoint].transform.GetChild(0).GetComponent<Renderer>().material = mat;
        checkpoints[currentCheckpoint].transform.GetChild(2).GetComponent<Renderer>().material = mat;
            
    }

    void Start()
    {
        //set checkpoints
        currentCheckpoint = 0;
        checkpoints = userFacingCheckpoints;
        
        //set original meterial
        originalMaterial = checkpoints[currentCheckpoint].transform.GetChild(0).GetComponent<Renderer>().material;

        //make newMaterial use originalMaterial as a base
        newMaterial = new Material(originalMaterial);

        //set initial checkpoint to newMaterial
        setCurrentCheckpointMaterial(newMaterial);

        // winTextObject.SetActive(false);
        winPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //increment total time
        totalTime += Time.deltaTime;

        /* Animation logic */
        /*
        *   We linear interpolate from startColor to endColor when t \in [0, 0.5)
        *   We linear interploate from endColor to startColor when t \in [0.5, 1)
        *   Calculate t: spot in animation, number of seconds into animation normalized by animationLength
        *   t * 2 or (t - 0.5f) * 2: map range of t to [0, 1]     
        */
        float t = (totalTime % animationLength) / animationLength;
        Color c;
        if (t < 0.5) {
            c = Color.Lerp(startColor, endColor, t * 2);
        } else {
            c = Color.Lerp(endColor, startColor, (t - 0.5f) * 2);
        }
        newMaterial.color = c;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("hit something");
        // if (other.gameObject.CompareTag("PickUp")) {
        //     other.gameObject.SetActive(false);
        //     count++;
        //     SetCountText();
        // }

        //if finished level, no need to check for collisions.
        if (finished) return;

        //checkpoint system - check if collision is next checkpoint and do something if not already done the level
        //check if we hit a checkpoint in the list or the thing we hit is a child of a thing in the list
        //this allows us to have a more complex checkpoint with a hitbox that we just put into the list
        if (other.gameObject == checkpoints[currentCheckpoint] || (other.gameObject.transform.parent != null &&  other.gameObject.transform.parent.gameObject == checkpoints[currentCheckpoint])) {
            Debug.Log("hit correct checkpoint number " + currentCheckpoint);
            
            //reset current checkpoint to original material
            setCurrentCheckpointMaterial(originalMaterial);
            
            
            //increment currentCheckpoint
            currentCheckpoint = (currentCheckpoint + 1) % checkpoints.Length;
            
            //play happy 'ding' noise - correct checkpoint crossed
            if (correctSoundAudioSource == null) {
                Debug.Log("Correct Checkpoint Sound Not Set.");
            } else {
                correctSoundAudioSource.Play();
            }

            //if we loop back to 0, we are finished.
            if (currentCheckpoint == 0) {
                Debug.Log("hit all checkpoints");
                finished = true;
                // winTextObject.SetActive(true);
                winPanel.SetActive(true);
                Time.timeScale = 0.50f;
                timer.deactivate();
                //set next level buttons active here
            }
            
            //set new current checkpoint to be animated material.
            setCurrentCheckpointMaterial(newMaterial);
        } else if (other.gameObject.tag == "checkpoint") {
            //we have hit a checkpoint that is the incorrect one - play unhappy 'error' noise
            if (warningSoundAudioSource == null) {
                Debug.Log("Wrong Checkpoint Warning Sound Not Set.");
            } else {
                warningSoundAudioSource.Play();
            }
        }
    }
}
