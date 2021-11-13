using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject _menus;
   
    public void SetMenu(bool enabled)
    {
        _menus.SetActive(enabled);

    }

    public void Play()
    {
        if (GameManager._loaded)
        {
            GameManager.PlayGame();
        }
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
