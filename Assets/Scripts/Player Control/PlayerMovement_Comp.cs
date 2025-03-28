using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Comp : MonoBehaviour
{
    // Input
    PlayerInput playerInput;
    InputAction moveAction;

    // Components
    Rigidbody rb;
    Animator anim;
    CapsuleCollider capsule;

    // Necessary variables
    float playerHeight;
    Vector2 inputDirection;
    bool grounded;
    bool moving;
    Vector3 moveDirection;
    bool sloped;
    Vector3 floorNormal;

    // Public variables
    public float moveSpeed;
    public float rotationSpeed;
    public float fallMultiplier;
    public LayerMask ground;

    // Audio
    public AudioSource runningAudioSource;
    public AudioClip runningSound;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components and the Move Input Action
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();

        // Set necessary values
        playerHeight = capsule.height * transform.localScale.y;
        IsGrounded();
        IsMoving();
        GetInputDirection();

        // ~
        if (runningAudioSource != null && runningSound != null)
        {
            runningAudioSource.clip = runningSound;
            runningAudioSource.loop = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInputDirection();
        AnimationController();
        SoundController();
        MovePlayer();
        
    }

    // FixedUpdate is called 50 times per second, regardless of the framerate
    private void FixedUpdate()
    {
        IsGrounded();
        IsMoving();
        GetFloorDirection();
        ApplyFalling();
    }

    // Controls animations by modulating Parameters on the Blend Tree
    private void AnimationController()
    {
        anim.SetBool("isMoving", moving);
        anim.SetBool("isGrounded", grounded);
        anim.SetFloat("horizontal", inputDirection.x, 1f, Time.deltaTime * 10f);
        anim.SetFloat("vertical", inputDirection.y, 1f, Time.deltaTime * 10f);
    }

    // Controls footstep sound effects
    private void SoundController()
    {
        bool isMovingForward = inputDirection.y > 0;

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
    }

    // Move and rotate the player depending on floor slope
    private void MovePlayer()
    {
        //Quaternion rotation = Quaternion.LookRotation(, floorNormal);

        Vector3 movement = (transform.right * inputDirection.x + transform.forward * inputDirection.y).normalized;
        Vector3 targetVelocity = movement * moveSpeed * 10 / transform.localScale.y;

        Vector3 velocity = rb.velocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.velocity = velocity;

        if (grounded && inputDirection == Vector2.zero)
        {
            rb.velocity = new Vector3(0f, velocity.y, 0f);
        }

        // Basic rotation control
        if (inputDirection.x != 0)
        {
            transform.Rotate(inputDirection.x * rotationSpeed * Time.deltaTime * Vector3.up);
        }
    }

    // Get the direction of movement depending on the angle of the floor surface
    private void GetFloorDirection()
    {
        Vector3 raycastOrigin = capsule.center;
        float raycastDistance = playerHeight / 2 + 0.2f;
        if (Physics.Raycast(raycastOrigin, Vector3.down, out RaycastHit hit, raycastDistance, ground))
        {
            if (hit.normal == Vector3.up)
            {
                sloped = false;
                floorNormal = Vector3.up;
            }
            else
            {
                sloped = true;
                floorNormal = hit.normal;
            }
        }
    }

    // Checks if the player is on the ground
    private void IsGrounded()
    {
        Vector3 raycastOrigin = capsule.center;
        float raycastDistance = playerHeight/2 + 0.2f;
        bool raycastCheck = Physics.Raycast(raycastOrigin, Vector3.down, raycastDistance, ground);
        grounded = raycastCheck;
    }

    // Checks if the player is moving
    private void IsMoving()
    {
        moving = rb.velocity != Vector3.zero;
    }

    // Gets the vector of the player's WASD input
    private void GetInputDirection()
    {
        // inputDirection.x accesses horizontal component: -left/right+
        // inputDirection.y accesses vertical component: -back/forward+
        inputDirection = moveAction.ReadValue<Vector2>();
    }

    private void ApplyFalling()
    {
        rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        //if (rb.velocity.y < 0)
        //{
        //    rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        //}
    }
}
