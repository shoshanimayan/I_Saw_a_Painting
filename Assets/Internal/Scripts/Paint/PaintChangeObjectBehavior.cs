using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintChangeObjectBehavior : MonoBehaviour
{
    [SerializeField] private Color _color;
    private bool _active;
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }
    private void Start()
    {
        GetComponent<Renderer>().material.SetColor("_Color", _color);
    }
    public void SetColor(Color color)
    {
        _color = color;
        GetComponent<Renderer>().material.SetColor("_Color", _color);
    }

    public void ChangeColor()
    {
        _manager.paintBombColor = _color;
    }

    private void Update()
    {

        if (GameManager.GetState() == GameState.Play)
        {
            if (!_active)
            {
                _active = true;
                 gameObject.SetActive(true); 

            }
        }
        else
        {
            if (_active)
            {
                _active = false;
                gameObject.SetActive(false); 
            }
        }

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Paint")
        {
            ChangeColor();
        }
        gameObject.SetActive(false);

    }
}
