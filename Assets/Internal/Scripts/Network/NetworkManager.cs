using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.Events;
using System;

public class NetworkManager : Singleton<NetworkManager>
{
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
       reference = FirebaseDatabase.DefaultInstance.RootReference;

    }

    
}
