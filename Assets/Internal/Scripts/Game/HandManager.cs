using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
public class HandManager : Singleton<HandManager>
{
    [SerializeField] private GameObject[] _hands;
    private bool _active;
    private XRRayInteractor _xrray;
    private LineRenderer _linerender;
    private XRInteractorLineVisual _xrLineVis;


    public void SetHandStatus(bool active)
    {
        foreach (GameObject hand in _hands)
        {
             _xrray = hand.GetComponent<XRRayInteractor>();
             _linerender = hand.GetComponent<LineRenderer>();
             _xrLineVis = hand.GetComponent<XRInteractorLineVisual>();

            if (active)
            {
                _linerender.enabled = true;
                _xrLineVis.enabled = true;
                _xrray.maxRaycastDistance = 5;
            }
            else
            {
                _linerender.enabled = false;
                _xrLineVis.enabled = false;
                _xrray.maxRaycastDistance = .5f;
            }
                
        }
    }


}
