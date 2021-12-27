using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////
    [SerializeField] private float _topY;
    [SerializeField] private float _speed=5;

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////
    private Vector3 _top;
    private Vector3 _origin;
    private bool _up;
    private bool _active;



    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////

    private void Start()
    {
        _origin = transform.position;
        _top = new Vector3(transform.position.x, _topY, transform.position.z);
        _up = true;

    }

    private void Update()
    {

        if (GameManager.GetState() != GameState.Menu)
        {
            if (!_active) { _active = true; }
            if (_up) 
            {
                transform.position = Vector3.MoveTowards(transform.position, _top,Time.deltaTime*_speed);
                if (Vector3.Distance(transform.position, _top) < .1f) 
                {
                    _up = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _origin, Time.deltaTime * _speed);
                if (Vector3.Distance(transform.position, _origin) < .1f)
                {
                    _up = true;
                }
            }
        }
        else {
            if (_active)
            {
                transform.position = _origin;
                _active = false;
            }
        }

    }
}
