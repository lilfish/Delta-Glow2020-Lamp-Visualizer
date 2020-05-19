using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class WalkRound : MonoBehaviour
{
    public int maxDistance = 40;
    [Range(0.1f, 2.0f)]
    public float speed = 1;

    Vector2 nextPosition = Vector2.zero;
    Vector3 nextPlazaPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = UnityEngine.Random.insideUnitCircle * maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 positionOnPlaza;
        positionOnPlaza.x = transform.position.x;
        positionOnPlaza.y = transform.position.z;

        if((positionOnPlaza - nextPosition).magnitude < 1)
        {
            nextPosition = UnityEngine.Random.insideUnitCircle * maxDistance;
        }

        Vector2 dVelocity = (positionOnPlaza - nextPosition).normalized * speed;
        Vector3 dPosition = transform.position;
        
        dPosition.x -= dVelocity.x;
        dPosition.z -= dVelocity.y;

        transform.position = dPosition;

        nextPlazaPosition.x = nextPosition.x;
        nextPlazaPosition.z = nextPosition.y;

        Quaternion dRotation = Quaternion.LookRotation(transform.position - nextPlazaPosition);
        Vector3 correctingYaw = dRotation.eulerAngles;
        correctingYaw.x = 0;
        dRotation.eulerAngles = correctingYaw;
        transform.rotation = dRotation;

    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawSphere(nextPlazaPosition, 1);
        Color dColor = Gizmos.color;
        dColor.a = 0.1f;
        Gizmos.color = dColor;
        Gizmos.DrawSphere(Vector3.zero, maxDistance);
    }

}
