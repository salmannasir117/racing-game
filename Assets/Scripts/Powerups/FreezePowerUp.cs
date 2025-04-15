using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerUp : MonoBehaviour
{
    //set to Player Character gameobject in inspector
    public GameObject player;
    //how long powerup lasts
    public float powerupDuration = 5.0f;
    //store if powerup is active
    private bool powerupActive;
    //store original move/rotation speed to reset it after powerup over.
    //calculate new speeds to for when powerup is active.
    private float originalMoveSpeed, originalRotationSpeed, newMoveSpeed, newRotationSpeed;

    public AudioSource freeze_sound;
    void Start()
    {
        //powerup is not active initially
        powerupActive = false;

        //store the original speed values
        originalMoveSpeed = player.GetComponent<PlayerMovement_Comp>().moveSpeed;
        originalRotationSpeed = player.GetComponent<PlayerMovement_Comp>().rotationSpeed;

        //set new speed values for when powerup is active
        newMoveSpeed = originalMoveSpeed * 0.0f;
        newRotationSpeed = originalRotationSpeed * 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //use powerupDuration as countdown timer.
        if (powerupActive) {
            powerupDuration -= Time.deltaTime;
        }
        
        //once powerup has been active for powerupDuration time, deactivate powerup.
        if (powerupDuration <= 0.0f) {
            //disable powerup
            deactivatePowerup();
        }
    }

    //activate powerup if player hits it
    void OnTriggerEnter(Collider other) {
        Debug.Log("speedup powerup trigger entered");

        //check if powerupActive to prevent multiple collisions having additive effect.
        if (other.gameObject == player && !powerupActive) {
            Debug.Log("player hit speedup powerup");
            activatePowerup();
        }
    }

    /* 
        Activating powerup means:
            setting powerupActive to true
            setting player speeds to the new speed to display a change
            disable the mesh renderer so that it is invisible to player. 
                cannot destroy yet as we need to use the timer in upate method.
    */
    void activatePowerup() {
        powerupActive = true;
        freeze_sound.Play();
        player.GetComponent<PlayerMovement_Comp>().moveSpeed = newMoveSpeed;
        player.GetComponent<PlayerMovement_Comp>().rotationSpeed = newRotationSpeed;
        GetComponent<MeshRenderer>().enabled = false;
    }

    /*
        deactivate powerup means:
            setting powerupActive false
            resetting player speeds
            destroying the gameobject. tt is no longer needed.
    */
    void deactivatePowerup() {
        powerupActive = false;
        player.GetComponent<PlayerMovement_Comp>().moveSpeed = originalMoveSpeed;
        player.GetComponent<PlayerMovement_Comp>().rotationSpeed = originalRotationSpeed;
        Destroy(gameObject);

    }
}
