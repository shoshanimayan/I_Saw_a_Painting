using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour
{

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////
    /// <summary>
    /// initaliaze connection to firebase database
    /// </summary>
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
                return;
            }
            
        });

    }

   
}
