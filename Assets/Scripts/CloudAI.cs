using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CloudAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject[] waypoints;
    private int currWaypoint = -1;
    public GameObject character; // Assign Character in Inspector
    public float triggerHeight = 500f;
    public float strikeDuration = 2f;

    private bool isStriking = false;
    private Vector3 originalScale; 
    private float lastStrikeTime = -10f; 
    public float strikeCooldown = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalScale = transform.localScale; // Store original cloud size
        SetNextWaypoint();
    }

    void Update()
    {
        float horizontalDistance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z), 
            new Vector3(character.transform.position.x, 0, character.transform.position.z)
        );

        //Debug.Log("Cloud Position: " + transform.position + " | Distance to Character: " + horizontalDistance);

        // Move between waypoints
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNextWaypoint();
        }

        // Check if character is vertically close
        if (Time.time >= lastStrikeTime + strikeCooldown && horizontalDistance < 50f)
        {
            StartCoroutine(StrikeLightning());
            
        }
    }

    void SetNextWaypoint()
    {
        if (waypoints.Length == 0) return;
        currWaypoint = (currWaypoint + 1) % waypoints.Length;
        Vector3 targetPos = waypoints[currWaypoint].transform.position;
        targetPos.y = transform.position.y;
        agent.SetDestination(targetPos);


    }

    IEnumerator StrikeLightning()
    {
        Debug.Log("Striking");
        lastStrikeTime = Time.time;

        // Store original scale and define stretched size
        Vector3 stretchedScale = new Vector3(originalScale.x, 500, originalScale.z); // Stretch downward
        Vector3 stretchedPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < strikeDuration / 2)
        {
            transform.localScale = Vector3.Lerp(originalScale, stretchedScale, elapsedTime / (strikeDuration / 2));
            transform.position = stretchedPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Shrink back
        elapsedTime = 0f;
        while (elapsedTime < strikeDuration / 2)
        {
            transform.localScale = Vector3.Lerp(stretchedScale, originalScale, elapsedTime / (strikeDuration / 2));
            transform.position = stretchedPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
