using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    void Start() {
        startPosition = this.gameObject.transform.position;
        startRotation = this.gameObject.transform.rotation;
    }
      
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Out of bounds")
        {
            this.gameObject.transform.position = startPosition;
            this.gameObject.transform.rotation = startRotation;
        }

    }
}
