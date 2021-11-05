using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<string> recording= new List<string>();

    private void Awake()
    {
        recording.Clear();
    }

    public static void AddSplash(string splashInfo)
    {
        recording.Add( splashInfo);

        Debug.Log(JsonUtility.ToJson(recording.ToArray()));


    }

    private static void Ended()
    {
        recording.Clear();
    }

    private static void PlayGame()
    { 
    
    }

    private static void PlayRecord()
    { 
    
    }




}
