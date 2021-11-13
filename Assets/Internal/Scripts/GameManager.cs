using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum GameState {Menu,Play,Auto,None }
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<string> _recording= new List<string>();
    private static GameState _State;
    private static HandManager _handManager { get { return HandManager.Instance; } }
    private static MenuManager _menuManager { get { return MenuManager.Instance; } }
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


    public static void AddSplash(string splashInfo)
    {
        _recording.Add( splashInfo);

    }

    public static void ToMenu()
    {
        _State = GameState.Menu;
        _recording.Clear();
        FindObjectOfType<XRRig>().transform.eulerAngles = Vector3.zero;
        _handManager.SetHandStatus(true);
        _menuManager.SetMenu(true);

    }

    public static void PlayGame()
    {
        ResetMaterials();
        _handManager.SetHandStatus(false);
        _menuManager.SetMenu(false);
        _State = GameState.Play;

    }

    public static void PlayRecord()
    {
        ResetMaterials();
        _handManager.SetHandStatus(false);
        _menuManager.SetMenu(false);
        _State = GameState.Auto;

    }

    public static GameState GetState()
    {
        return _State;
    }

    

}
