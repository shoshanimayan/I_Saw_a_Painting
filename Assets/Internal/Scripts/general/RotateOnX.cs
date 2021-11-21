using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnX : MonoBehaviour
{
    private Quaternion _origin;
    private bool _active;
    [SerializeField] private int _speed = 25;
    [SerializeField] private bool _forward;

    private void Start()
    {
        _origin = transform.rotation;
    }
    void Update()
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
