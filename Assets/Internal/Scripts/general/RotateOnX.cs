using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnX : MonoBehaviour
{
    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////
    [SerializeField] private int _speed = 25;
    [SerializeField] private bool _forward;

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////

    private Quaternion _origin;
    private bool _active;

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////

    private void Start()
    {
        _origin = transform.rotation;
    }
    private void Update()
    {

        if (GameManager.GetState() != GameState.Menu)
        {
            if (!_active) { _active = true; }
            if (!_forward)
            {
                transform.Rotate(transform.right, _speed * Time.deltaTime);
            }
            else 
            {
                transform.Rotate(transform.up, _speed * Time.deltaTime);
            }
        }
        else 
        {
            if (_active)
            {
                transform.rotation = _origin;
                _active = false;
            }

        }

    }
}
