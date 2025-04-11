using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(CapsuleCollider))]
[RequireComponent(typeof(NavMeshAgent))]

public class Kitty_AI : MonoBehaviour
{
    private NavMeshAgent kitnav;
    private CapsuleCollider kitcap;
    private Animator kitani;

    public GameObject player;

    public enum AIState
    {
        Wait,
        Walk,
        Run
    }

    private AIState kitstate;

    // Start is called before the first frame update
    void Start()
    {
        kitnav = GetComponent<NavMeshAgent>();
        kitcap = GetComponent<CapsuleCollider>();
        kitani = GetComponent<Animator>();

        kitstate = AIState.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        kitnav.SetDestination(player.transform.position);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            kitstate = AIState.Wait;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            kitstate = AIState.Walk;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            kitstate = AIState.Run;
        }
        AnimationController();
    }

    private void AnimationController()
    {
        switch (kitstate)
        {
            case AIState.Wait:
                kitani.SetFloat("Vert", 0f);
                kitani.SetFloat("State", 0f);
                break;
            case AIState.Walk:
                kitani.SetFloat("Vert", 1f);
                kitani.SetFloat("State", 0f);
                break;
            case AIState.Run:
                kitani.SetFloat("Vert", 1f);
                kitani.SetFloat("State", 1f);
                break;

        }
        kitani.SetFloat("State", 1f);
    }
}
