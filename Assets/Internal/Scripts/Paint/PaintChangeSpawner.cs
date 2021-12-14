using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintChangeSpawner : MonoBehaviour
{

    [SerializeField] private Color[] _colors;
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;
    [SerializeField] private PaintChangeObjectBehavior _colorChangeObject;
    private PaintProjectileManager _manager { get { return PaintProjectileManager.Instance; } }
    private int _lastColorIndex = -1;
    private void Awake()
    {
        _colorChangeObject.gameObject.SetActive(false);
    }

    private void Start()
    {
        InvokeRepeating("SpawnColorObject", 2f, 8f);
    }


    private void SpawnColorObject()
    {
        if (GameManager.GetState() == GameState.Play)
        {
            if (!_colorChangeObject.gameObject.activeSelf) { _colorChangeObject.gameObject.SetActive(true); }
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(1, size.y), Random.Range(-size.z / 2, size.z / 2));
            _colorChangeObject.SetColor(GetRandomColor());
            _colorChangeObject.transform.position = pos;
        }
        else {
            if (_colorChangeObject.gameObject.activeSelf) 
            { 
                _colorChangeObject.gameObject.SetActive(false);
            }
        }

    }

    private Color GetRandomColor()
    {
        int colorIndex= Random.Range(0, _colors.Length);
        while (colorIndex != _lastColorIndex && _colors[colorIndex]== _manager.paintBombColor)
        {
            colorIndex = Random.Range(0, _colors.Length);
        }
        return _colors[colorIndex];

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition+center, size);
    }
}
