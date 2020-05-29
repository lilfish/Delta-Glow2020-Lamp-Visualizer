using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    float inputX, inputZ;
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        if(inputX != 0)
            moveSide();
        if(inputZ != 0)
            moveNorm();
    }

    private void moveSide() // a & d
    {
        transform.position += transform.right * speedH * inputX;
    }
    private void moveNorm() // w & s
    {
        transform.position += transform.forward * speedH * inputZ;
    }
}
