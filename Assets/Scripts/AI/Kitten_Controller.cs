using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Kitten_Controller : MonoBehaviour
{
    private Animator kit_ani;
    private CapsuleCollider kit_cap;
    private NavMeshAgent kit_nav;
    private Rigidbody kit_rbd;
    private AudioSource kit_aud;

    private AIState kit_sta;
    private GameObject player;
    private Rigidbody player_rbd;

    public AudioClip meowSound;

    private float dist;
    private bool found;

    public enum AIState
    {
        Wait,   // Before the player finds me
        Idle,   // When the player is standing still
        Walk,   // When the player is walking
        Run     // When the player is way far
    }

    // Start is called before the first frame update
    void Start()
    {
        kit_ani = GetComponent<Animator>();
        kit_cap = GetComponent<CapsuleCollider>();
        kit_nav = GetComponent<NavMeshAgent>();
        kit_rbd = GetComponent<Rigidbody>();
        kit_aud = GetComponent<AudioSource>();

        kit_sta = AIState.Wait;

        player = GameObject.FindGameObjectWithTag("player");
        player_rbd = player.GetComponent<Rigidbody>();

        dist = (this.transform.position - player.transform.position).magnitude;
        found = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = (this.transform.position - player.transform.position).magnitude;

        if (!found)
        {
            IsPlayerNear();
        }
        else
        {
            ActivityController();
        }

        AnimationControl();
    }

    private void AnimationControl()
    {
        switch (kit_sta)
        {
            case AIState.Wait:
                kit_ani.SetFloat("Vert", 0.6f);
                kit_ani.SetFloat("State", 0f);
                break;
            case AIState.Idle:
                kit_ani.SetFloat("Vert", 0f);
                kit_ani.SetFloat("State", 0f);
                break;
            case AIState.Walk:
                kit_ani.SetFloat("Vert", 1f);
                kit_ani.SetFloat("State", 0f);
                break;
            case AIState.Run:
                kit_ani.SetFloat("Vert", 1f);
                kit_ani.SetFloat("State", 1f);
                break;
        }
    }

    private bool IsPlayerNear()
    {
        
        if (dist <= 20)
        {
            kit_aud.clip = meowSound;
            kit_aud.Play();
            found = true;
            print(name + " found!");
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ActivityController()
    {
        // Set the current state
        if (player_rbd.velocity.magnitude >= 0.2)
        {
            kit_sta = AIState.Walk;
            kit_nav.SetDestination(player.transform.position);
            print(name + " -> Walk");
        }
        else if (player_rbd.velocity.magnitude < 0.2)
        {
            if (dist <= 1)
            {
                kit_nav.SetDestination(player.transform.position);
                kit_sta = AIState.Idle;
                print(name + " -> Idle");
            }
            else
            {
                kit_nav.SetDestination(player.transform.position);
                kit_sta = AIState.Walk;
                print(name + " -> Run");
            }
        }
        if (dist >= 15)
        {
            kit_nav.SetDestination(player.transform.position);
            kit_sta = AIState.Run;
            print(name + " -> Run");
        }
    }
}
