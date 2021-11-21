using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{



    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }

    //Enter the Speed of the Bullet from the Component Inspector.
    [SerializeField] private float Bullet_Forward_Force;



    private void Start()
    {
        InvokeRepeating("LaunchProjectile", 2f, 3f);

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
