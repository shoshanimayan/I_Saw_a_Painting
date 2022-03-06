using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
public class AutoPaintManager : Singleton<AutoPaintManager>
{

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }

    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////

    [SerializeField] private string _name;
    [SerializeField] private int _alphaNum;
    [SerializeField] private Color _color;
    [SerializeField] private Vector2 _coor;

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////

    private IEnumerator PaintSplashes( List<SplashInfo> splashes)
    {
        foreach (SplashInfo splash in splashes)
        {
            AutoPaint(splash._name, splash._coordinate, splash._color, splash._alphaNumber);
            yield return null;
        }
        yield return null;

    }

    private void AutoPaint(string name, Vector2 coordinate, Color color, int alpha)
    {
        GameObject obj = GameObject.Find(name);
       obj.GetComponent<MyShaderBehavior>().PaintOnColored(coordinate, _manager.GetProjectileSplash(alpha), color);
    }

    //////////////////
    //  PUBLIC API  //
    /////////////////

    public void RetrieveJson(int key)
    {
        List<SplashInfo> splashes = new List<SplashInfo>();
        FirebaseDatabase.DefaultInstance
            .GetReference("Paintings/" + (key).ToString())
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.LogError(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
               
                    foreach (DataSnapshot child in snapshot.Children)
                    {
                        splashes.Add(JsonUtility.FromJson<SplashInfo>(child.Value.ToString()));
                    }
                    StartCoroutine(PaintSplashes(splashes));

                }
            });
    }
}
