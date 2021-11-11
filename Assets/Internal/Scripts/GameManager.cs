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
    private void Awake()
    {

        _State = GameState.None;
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

    }

    public static void PlayGame()
    {
        ResetMaterials();

        _State = GameState.Play;

    }

    public static void PlayRecord()
    {
        ResetMaterials();
        _State = GameState.Auto;

    }

    public static GameState GetState()
    {
        return _State;
    }

    

}
