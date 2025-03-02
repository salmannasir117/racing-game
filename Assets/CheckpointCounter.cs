using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCounter : MonoBehaviour
{
    public GameObject[] userFacingCheckpoints;
    public static GameObject[] checkpoints;
    public static int currentCheckpoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = userFacingCheckpoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
