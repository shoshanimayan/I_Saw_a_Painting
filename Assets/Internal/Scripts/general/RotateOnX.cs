using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnX : MonoBehaviour
{
    private int _direction = 1;
    private Quaternion _origin;
    private bool _active;
    [SerializeField] private int _speed = 25;
    [SerializeField] private int _zAxis = 0;
    [SerializeField] private int _yAxis = 0;

    private void Start()
    {
        _origin = transform.rotation;
    }
    void Update()
    {
        if (GameManager.GetState() != GameState.Menu)
        {
            if (!_active) { _active = true; }
            transform.Rotate(_direction * _speed * Time.deltaTime, _yAxis, _zAxis);
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
