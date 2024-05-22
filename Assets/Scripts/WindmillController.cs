using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : MonoBehaviour
{
    public float spinSpeed;
    private Rigidbody windmill;
    void Start()
    {
        windmill = GetComponent<Rigidbody>();
        windmill.maxAngularVelocity = spinSpeed;
    }

    void Update()
    {
        windmill.AddTorque(Vector3.forward*1000, ForceMode.Impulse);
    }
}
