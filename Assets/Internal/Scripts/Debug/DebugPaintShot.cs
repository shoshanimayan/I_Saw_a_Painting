using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPaintShot : MonoBehaviour
{

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////
    private void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;  
    }

    private void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 10;

    }
}
