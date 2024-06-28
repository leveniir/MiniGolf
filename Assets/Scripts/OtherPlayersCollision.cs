using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayersCollision : MonoBehaviour
{
    Vector3 ballPosition;
    private Rigidbody thisRigidbody;
    string objectName;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {    
        if (collision.collider.tag == "Out of bounds")
        {  
            objectName = gameObject.name;
            ballPosition = new Vector3 (LevelManager.Instance.ballsData[name][3], LevelManager.Instance.ballsData[name][4], LevelManager.Instance.ballsData[name][5]);
            thisRigidbody.position = ballPosition;
            thisRigidbody.velocity = Vector3.zero;
            thisRigidbody.angularVelocity = Vector3.zero;
        }
    }
}

