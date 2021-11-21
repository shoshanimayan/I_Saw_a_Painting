using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPaintShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;  
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 10;

    }
}
