using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPaintManager : MonoBehaviour
{
    //need
    /// <summary>
    /// need gameobject to get script - get gameobject name
    /// color
    ///  texture coordinate vector2 
    /// number for alpha texture to retrieve
    /// 
    /// send json to here, parse, and apply
    /// </summary>
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }
    [SerializeField] private string _name;
    [SerializeField] private int _alphaNum;
    [SerializeField] private Color _color;
    [SerializeField] private Vector2 _coor;


    // Start is called before the first frame update
    private void Start()
    {

        AutoPaint(_name, _coor, _color,_alphaNum);
    }

    private void AutoPaint(string name, Vector2 coordinate, Color color, int alpha)
    {
        GameObject obj = GameObject.Find(name);
        MyShaderBehavior script = obj.GetComponent<MyShaderBehavior>();
        script.PaintOnColored(coordinate, _manager.GetProjectileSplash(alpha), color);
    }
}
