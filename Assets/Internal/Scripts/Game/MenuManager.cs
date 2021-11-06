using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _menus;
    private bool _active;
    // Update is called once per frame
    void Update()
    {
            if (GameManager.GetState() == GameState.Menu)
            {
                if (!_active)
                {
                    foreach (GameObject obj in _menus)
                    {
                        obj.SetActive(true);
                    }
                    _active = true;
                }
            }
            else
            {
                if (_active)
                {
                    foreach (GameObject obj in _menus)
                    {
                        obj.SetActive(false);
                    }
                    _active = false;
                }
            }
    }

    public void Play()
    {
        GameManager.PlayGame();
    }
    public void RunAuto()
    {
        GameManager.PlayRecord();
    }

    public void EndGame()
    {
        GameManager.Ended();
    }
}
