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
    public GameObject character; 

    public GameObject yellowLightning;
    public GameObject redLightning;
    public float strikeDuration = 0.5f;

    private bool isStriking = false;
    private Vector3 originalScale; 
    private float lastStrikeTime = -10f; 
    public float strikeCooldown = 10f;
    public AudioSource lightningAudio;

    private enum AIState { Idle, Patrol, Chase }
    private AIState currentState = AIState.Patrol;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        originalScale = transform.localScale; // Store original cloud size
        SetNextWaypoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // Do nothing or wait
                break;

            case AIState.Patrol:
                PatrolBehavior();
                break;

            case AIState.Chase:
                ChaseBehavior();
                break;
        }

        CheckTransitions(); // Determine when to switch states
    }

    /*
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
            isStriking = false;
            StartCoroutine(StrikeLightning());
            SetNextWaypoint();
        }
    }
    */

    void PatrolBehavior()
{
    if (!agent.pathPending && agent.remainingDistance < 0.5f)
    {
        isStriking = false;
        StartCoroutine(StrikeLightning());
        SetNextWaypoint();
    }
}

void ChaseBehavior()
{
    if (character != null)
    {
        Vector3 chaseTarget = character.transform.position;
        chaseTarget.y = transform.position.y; // Keep cloud floating
        agent.SetDestination(chaseTarget);
    }
}

void CheckTransitions()
{
    float horizontalDistance = Vector3.Distance(
        new Vector3(transform.position.x, 0, transform.position.z),
        new Vector3(character.transform.position.x, 0, character.transform.position.z)
    );

    switch (currentState)
    {
        case AIState.Idle:
            if (horizontalDistance > 50f)
                currentState = AIState.Patrol;
            break;

        case AIState.Patrol:
            if (horizontalDistance < 20f)
                currentState = AIState.Chase;
            break;

        case AIState.Chase:
            if (horizontalDistance > 50f)
                currentState = AIState.Patrol;
            break;
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
        // Set context for volume
        bool wasChasing = isStriking; // Capture reason before resetting it

        Debug.Log("Striking");
        lastStrikeTime = Time.time;

        GameObject lightning = yellowLightning;
        if(isStriking){
            lightning = redLightning;
        }

        lightningAudio.volume = wasChasing ? 1f : 0.1f;
        lightningAudio.Play();

        Vector3 stretchedScale = new Vector3(originalScale.x, 500, originalScale.z);
        Vector3 stretchedPosition = transform.position;

        float elapsedTime = 0f;
        while (elapsedTime < strikeDuration / 2)
        {
            lightning.SetActive(true);
            lightning.transform.position = transform.position;
            transform.position = stretchedPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < strikeDuration / 2)
        {
            lightning.SetActive(false);
            lightning.transform.position = transform.position;
            transform.position = stretchedPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isStriking = false;
    }


    void OnTriggerEnter(Collider other)
    {
        
        // Check if the lightning collides with the character
        if (redLightning != null && other.gameObject == character && Time.time >= lastStrikeTime)
        {
            isStriking = true;
            StartCoroutine(StrikeLightning()); 
        }
    }
}
