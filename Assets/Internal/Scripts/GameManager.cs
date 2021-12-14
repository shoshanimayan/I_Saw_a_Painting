using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
public enum GameState {Menu,Play,Auto,None }
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<string> _recording= new List<string>();
    private static GameState _State;
    private static HandManager _handManager { get { return HandManager.Instance; } }
    private static MenuManager _menuManager { get { return MenuManager.Instance; } }
    private static AudioManager _audioManager { get { return AudioManager.Instance; } }

    private static AutoPaintManager _autoManager { get { return AutoPaintManager.Instance; } }
    private static int _currentIndex=-1;
    public static bool _loaded;
    private void Awake()
    {
        _State = GameState.Menu;
    }

    private static void ResetMaterials()
    {
        foreach (MyShaderBehavior painted in Resources.FindObjectsOfTypeAll(typeof(MyShaderBehavior)) as MyShaderBehavior[])
        {
            painted.RestartColor();
        }
    }

    private static void SendJsonInfo() 
    {

        if (_currentIndex != -1 &&_recording.Count>0)
        {
            Debug.Log(_currentIndex);
            Debug.Log((_currentIndex + 1).ToString());

            FirebaseDatabase.DefaultInstance
             .GetReference("Paintings").Child((_currentIndex+1).ToString()).SetValueAsync(_recording.ToArray()).ContinueWithOnMainThread(task => {
                 if (task.IsFaulted)
                 {
                     Debug.LogError(task.Exception);
                     _recording.Clear();
                 }
                 else if (task.IsCompleted)
                 {
                     _menuManager.SetTitle( (_currentIndex).ToString());
                     _currentIndex++;
                 }
             });
        }
    }

    public static void AddSplash(string splashInfo)
    {
        _recording.Add( splashInfo);

    }



    public static void ToMenu()
    {
        _audioManager.StopMainTheme();
        if (_State == GameState.Play)
        {
            SendJsonInfo();
        }
        _State = GameState.Menu;
        FindObjectOfType<XRRig>().transform.eulerAngles = Vector3.zero;
        _handManager.SetHandStatus(true);
        _menuManager.SetMenu(true);

    }

    public static void PlayGame()
    {
        _audioManager.PlayMainTheme();
        _audioManager.PlayClip("press");
        ResetMaterials();
        _handManager.SetHandStatus(false);
        _menuManager.SetMenu(false);
        _State = GameState.Play;

    }

    public static void PlayRecord(int key)
    {
        _audioManager.PlayClip("press");
        ResetMaterials();
        _handManager.SetHandStatus(false);
        _menuManager.SetMenu(false);
        _State = GameState.Auto;
        _autoManager.RetrieveJson(key);


    }

    public static GameState GetState()
    {
        return _State;
    }

    public static void SetIndex(int index) 
    {
        if (_currentIndex < index)
        {
            _currentIndex = index;
        }
    }

    

}
