using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float movementZ;    //vertical or jump movement

    // Start is called before the first frame update
    void Start() {
        count = 0;
        rb = GetComponent<Rigidbody>();                             //just need to get once and store it for the script
        
        SetCountText();
        winTextObject.SetActive(false);
    }

    //capture the input, store individual directions of movement
    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // void OnFire(InputValue fireValue) {
    //     winTextObject.SetActive(true);
    // }

    void OnJump(InputValue inputValue) {
        if (transform.position.y == 0.5f) {
            movementZ = 20.0f;
        }

    }
    void SetCountText() {
        countText.text = "Count: " + count.ToString();
        if (count >= 12) {
            winTextObject.SetActive(true);
        }
    }
    
    //move the ball
    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, movementZ, movementY);
        rb.AddForce(movement * speed);
        
        if (movementZ != 0) movementZ = 0;  //reset jump flag
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
