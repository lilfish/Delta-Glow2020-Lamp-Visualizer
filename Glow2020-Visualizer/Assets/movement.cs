using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float speedMultiplier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        if (Input.GetKey("j")){
            yaw = yaw - 10 * speedMultiplier;
        } else if (Input.GetKey("l")){
            yaw = yaw + 10 * speedMultiplier;
        }
        if (Input.GetKey("i")){
            pitch = pitch - 10 * speedMultiplier;
        } else if (Input.GetKey("k")){
            pitch = pitch + 10 * speedMultiplier;
        }

        if (Input.GetKey("o")){
            speedMultiplier = speedMultiplier + 0.25f;
        } else if (Input.GetKey("p")){
            speedMultiplier = speedMultiplier - 0.25f;
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
