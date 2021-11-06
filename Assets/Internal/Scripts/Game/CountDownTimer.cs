using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private float _timerTime;
    private float _currentTime;
    private bool _active;

    private void EndTimer()
    {
        GameManager.Ended();
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
            if (GameManager.GetState() == GameState.Play)
            {
                _active = true; 
            }
        }
    }


    public void StartTimer()
    {
        _currentTime = 0;
        _active = true;
    }
}
