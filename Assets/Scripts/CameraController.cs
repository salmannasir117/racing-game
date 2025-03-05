using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;        //calculate the offset between the camera transform position and the player transform position
    }

    // Update is called once per frame
    // use late update because this runs after all the other updates. so camera updated after player moves.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;        //camera position is player position plus the calculated offset
    }
}
