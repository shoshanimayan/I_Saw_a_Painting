using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////
    [SerializeField] private float _timerTime;
    [SerializeField] private float Bullet_Forward_Force;

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////

    private float _currentTime;
    private bool _active;
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////
    
    private void FixedUpdate()
    {
        if (_active)
        {
            if (_currentTime <= _timerTime)
            {
                _currentTime += Time.fixedDeltaTime;
            }
            else
            {
                LaunchProjectile();
                _currentTime = 0;
            }
        }
        else
        {
            if (GameManager.GetState() == GameState.Play && !_active)
            {
                _active = true;
                _currentTime = 0;
            }
        }
    }
    private void LaunchProjectile()
    {
        if (GameManager.GetState() == GameState.Play)
        {
            GameObject Temporary_Bullet_Handler = Instantiate(_manager.paintBombPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Rigidbody Temporary_RigidBody;
            Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
            Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
        }
    }

    
}
