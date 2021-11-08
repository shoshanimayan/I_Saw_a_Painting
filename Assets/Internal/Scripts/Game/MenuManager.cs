using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menus;
    private bool _active;
    // Update is called once per frame
    void Update()
    {
            if (GameManager.GetState() == GameState.Menu)
            {
                if (!_active)
                {
                _menus.SetActive(true);
                    _active = true;
                }
            }
            else
            {
                if (_active)
                {
                _menus.SetActive(false);
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
        GameManager.ToMenu();
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
