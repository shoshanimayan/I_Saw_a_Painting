using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintChangeObjectBehavior : MonoBehaviour
{

    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////

    [SerializeField] private Color _color;

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////

    private void Start()
    {
        GetComponent<Renderer>().material.SetColor("_Color", _color);
    }

    //////////////////
    //  PUBLIC API  //
    /////////////////

    public void SetColor(Color color)
    {
        _color = color;
        GetComponent<Renderer>().material.SetColor("_Color", _color);
    }

    public void ChangeColor()
    {
        _manager.paintBombColor = _color;
        gameObject.SetActive(false);
    }

   

        
        
    

}
