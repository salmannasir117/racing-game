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

    public GameObject lightning;
    public float strikeDuration = 0.5f;

    private bool isStriking = false;
    private Vector3 originalScale; 
    private float lastStrikeTime = -10f; 
    public float strikeCooldown = 10f;
    public AudioSource lightningAudio;

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
            isStriking = false;
            StartCoroutine(StrikeLightning());
            SetNextWaypoint();
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
        if(isStriking){
            Debug.Log("Striking");
            lastStrikeTime = Time.time;
        }

        // Store original scale and define stretched size
        Vector3 stretchedScale = new Vector3(originalScale.x, 500, originalScale.z); // Stretch downward
        Vector3 stretchedPosition = transform.position;

        float elapsedTime = 0f;
        lightningAudio.volume = isStriking ? 1f : 0.1f;
        lightningAudio.Play();
        while (elapsedTime < strikeDuration / 2)
        {
            lightning.SetActive(true);
            lightning.transform.position = transform.position;
            transform.position = stretchedPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Shrink back
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
        if (lightning != null && other.gameObject == character && Time.time >= lastStrikeTime)
        {
            isStriking = true;
            StartCoroutine(StrikeLightning()); 
        }
    }
}
