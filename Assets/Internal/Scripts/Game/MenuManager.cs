using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menus;
    private bool _active;
    private bool _loaded;
    // Update is called once per frame

    private void Awake()
    {
        _menus.SetActive(false);

    }
    private void Update()
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
