using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, SlowDown, ExtraTime, LessTime }
    public PowerUpType powerUpType; //Assign this in the Inspector??

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            ApplyPowerUp(c.gameObject);
            Destroy(gameObject); //Remove the power-up or power down after use
        }
    }

    private void ApplyPowerUp(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb == null) return; // Ensure the player has a Rigidbody component

        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(AdjustSpeed(rb, 1.5f, 5f)); //Increase speed by 1.5x for 5 seconds
                break;
            case PowerUpType.SlowDown:
                StartCoroutine(AdjustSpeed(rb, 0.5f, 5f)); //Decrease speed by 50% for 5 seconds
                break;
            case PowerUpType.ExtraTime:
                Timer.AddTime(10); //Add 10 seconds to the game timer
                break;
            case PowerUpType.LessTime:
                Timer.ReduceTime(10); //Reduce 10 seconds from the game timer
                break;
            default:
                Debug.Log("Unknown Power-Up");
                break;
        }
    }

    private IEnumerator AdjustSpeed(Rigidbody rb, float multiplier, float duration)
    {
        float originalSpeed = rb.velocity.magnitude;
        rb.velocity *= multiplier; // Adjust speed
        yield return new WaitForSeconds(duration);
        rb.velocity = rb.velocity.normalized * originalSpeed; //Restore speed
    }
}

// A simple static timer class for adding/removing time
public static class Timer
{
    public static float gameTime = 60f; //Example: Start with 60 seconds

    public static void AddTime(float amount)
    {
        gameTime += amount;
        Debug.Log("Time Added! Current Time: " + gameTime);
    }

    public static void ReduceTime(float amount)
    {
        gameTime = Mathf.Max(0, gameTime - amount);
        Debug.Log("Time Reduced! Current Time: " + gameTime);
    }
}

