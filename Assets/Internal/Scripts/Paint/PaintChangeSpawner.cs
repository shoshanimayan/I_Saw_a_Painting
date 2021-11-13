using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintChangeSpawner : MonoBehaviour
{

    [SerializeField] private Color[] _colors;
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;
    [SerializeField] private PaintChangeObjectBehavior _colorChangeObject;
    private int _lastColorIndex = -1;
    private bool _active;
    private void Awake()
    {
        _colorChangeObject.gameObject.SetActive(false);
    }

    private void Start()
    {
        InvokeRepeating("SpawnColorObject", 2f, 8f);
    }
    // Update is called once per frame
    /*void Update()
    {
        if (GameManager.GetState() == GameState.Play)
        {
            if (!_active)
            {
                _active = true;
                InvokeRepeating("SpawnColorObject", 2f, 8f);
            }
        }
        else
        {
            if (_active)
            {
                _active = false;
                _colorChangeObject.gameObject.SetActive(false);
                CancelInvoke();
            }
        }

    }*/

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
            if (_colorChangeObject.gameObject.activeSelf) { _colorChangeObject.gameObject.SetActive(false); }
        }

    }

    private Color GetRandomColor()
    {
        int colorIndex= Random.Range(0, _colors.Length);
        while (colorIndex == _lastColorIndex)
        {
            colorIndex = Random.Range(0, _colors.Length);
        }
        _lastColorIndex = colorIndex;
        return _colors[colorIndex];

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition+center, size);
    }
}
