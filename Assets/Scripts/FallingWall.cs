using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    Rigidbody rb;

    void Start() {
        startPosition = this.gameObject.transform.position;
        startRotation = this.gameObject.transform.rotation;
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
      
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Out of bounds")
        {
            this.gameObject.transform.position = startPosition;
            this.gameObject.transform.rotation = startRotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }
}
