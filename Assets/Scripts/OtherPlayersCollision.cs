using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayersCollision : MonoBehaviour
{
    public ControllingBalls controllingBalls;
    Vector3 ballPosition;
    private Rigidbody thisRigidbody;
    string objectName;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {    
        objectName = gameObject.name;
        ballPosition = controllingBalls.GetLastPosition(objectName);

        if (collision.collider.tag == "Out of bounds")
        {  
            thisRigidbody.position = ballPosition;
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
        }
    }
}

