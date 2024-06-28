using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayersCollision : MonoBehaviour
{
    public ControllingBalls controllingBalls;
    Vector3 ballPosition;

     void OnCollisionEnter(Collision collision)
    {    
        GameObject collidedObject = collision.gameObject;
        Rigidbody collidedRigidbody = collidedObject.GetComponent<Rigidbody>();

        ballPosition = controllingBalls.GetLastPosition(collidedObject.name);

        if(collision.collider.tag == "Out of bounds"){
            collidedRigidbody.position = ballPosition;
            collidedRigidbody.velocity = Vector3.zero;
            collidedRigidbody.angularVelocity = Vector3.zero;
        
        }
   
    }
}
