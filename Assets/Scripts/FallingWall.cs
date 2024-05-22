using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWall : MonoBehaviour
{
    public GameObject wall;
    void Start() {
        InvokeRepeating("LaunchProjectile", 0f, 2f);
    }
    
    void LaunchProjectile () {
        Instantiate(wall, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Out of bounds")
        {
            Destroy(wall);
        }

    }
}
