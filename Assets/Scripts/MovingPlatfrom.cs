using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    public GameObject gameObjec;
    private void  OnTriggerEnter(Collider other) {
        gameObjec.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other) {
            gameObjec.transform.parent = null;

    }
}
