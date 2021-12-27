using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////
    [SerializeField] private float _timerTime;

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////

    private float _currentTime;
    private bool _active;

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////
    private void EndTimer()
    {
        GameManager.ToMenu();
        _currentTime = 0;
        _active = false;

    }

    private void Update()
    {
        if (_active)
        {
            if (_currentTime <= _timerTime)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                EndTimer();
            }
        }
        else
        {
            if (GameManager.GetState() == GameState.Play && !_active)
            {
                _active = true; 
            }
        }
    }

    //////////////////
    //  PUBLIC API  //
    /////////////////
    public void StartTimer()
    {
        _currentTime = 0;
        _active = true;
    }
}
