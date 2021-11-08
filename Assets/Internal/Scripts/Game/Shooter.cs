using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;

    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;


    private bool _isActive;

    private void Start()
    {
        _isActive = false;
    }

    private void LaunchProjectile()
    {
         ;
        GameObject Temporary_Bullet_Handler = Instantiate(Bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
        Temporary_RigidBody.AddForce(transform.forward * Bullet_Forward_Force);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.GetState() == GameState.Play)
            {
                GameManager.ToMenu();
            }
            else { GameManager.PlayGame(); }
        }

        if (GameManager.GetState() == GameState.Play)
        {
            if (!_isActive) 
            {
                _isActive = true;
                InvokeRepeating("LaunchProjectile", 2f, 3f);
            }
        }
        else {
            if (_isActive)
            {
                _isActive = false;
                CancelInvoke();
            }
        }
    }
}
