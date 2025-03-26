using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction moveAction;

    Rigidbody rb;
    Animator anim;

    public GameObject directionAnchor;
    
    public float moveSpeed;
    public float rotationSpeed;
    private Vector3 forwardDirection;

    public AudioSource runningAudioSource;
    public AudioClip runningSound;
    

    //private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        moveAction = playerInput.actions.FindAction("Move");

        forwardDirection = getForwardDirection();
        if (runningAudioSource != null && runningSound != null)
        {
            runningAudioSource.clip = runningSound;
            runningAudioSource.loop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        AnimationController();
    }

    void MovePlayer()
    {
        // direction.x is the horizontal component (), direction.y is the vertical component
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();

        forwardDirection = getForwardDirection();

        if (inputDirection.y != 0)
        {
            transform.position += inputDirection.y * forwardDirection * moveSpeed * Time.deltaTime;
        }

        if (inputDirection.x != 0)
        {
            transform.Rotate(Vector3.up * inputDirection.x * rotationSpeed * Time.deltaTime);
        }
    }

    private void AnimationController()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        bool isMoving = rb.velocity.x != 0 && rb.velocity.z != 0;
        anim.SetBool("isMoving", isMoving);

        anim.SetFloat("horizontal", direction.x, 1f, Time.deltaTime * 10f);
        anim.SetFloat("vertical", direction.y, 1f, Time.deltaTime * 10f);

        bool isMovingForward = direction.y > 0;

        if (isMovingForward)
        {
            if (!runningAudioSource.isPlaying)
                runningAudioSource.Play();
        }
        else
        {
            if (runningAudioSource.isPlaying)
                runningAudioSource.Stop();
        }
        //anim.SetBool("isGrounded", isGrounded());
    }

    private Vector3 getForwardDirection()
    {
        Vector3 forwardDirection = directionAnchor.transform.position - transform.position;
        forwardDirection.y = 0;
        forwardDirection.Normalize();
        return forwardDirection;
    }

    private bool isGrounded()
    {
        return false;
    }
}
