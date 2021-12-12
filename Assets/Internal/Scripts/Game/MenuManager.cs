using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private GameObject _menus;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private TextMeshProUGUI _titleText;

    [SerializeField] private GameObject _ScrollContent;
    [SerializeField] private GameObject _ScrollButtonPrefab;
    [SerializeField] InputActionReference controllerActionGrip;

    private void Awake()
    {
        controllerActionGrip.action.started += EndPlayManual;
    }

    public void SetMenu(bool enabled)
    {
        _menus.SetActive(enabled);
        if (enabled)
        {
            SetAutoMenu();
        }
    }

    public void Play()
    {
        if (GameManager._loaded)
        {
            GameManager.PlayGame();
        }
    }

    public void SetTitle(string title)
    {
        _titleText.text = "Uploaded " + title;
    }

    public void RunAuto(int key)
    {
        GameManager.PlayRecord(key);
    }

    public void EndGame()
    {
        GameManager.ToMenu();
    }

    private void EndPlayManual(InputAction.CallbackContext context)
    {
        if (GameManager.GetState() != GameState.Menu)
        {
            EndGame();
        }

    }

    private void SetAutoMenu()
    {

        FirebaseDatabase.DefaultInstance
         .GetReference("Paintings")
         .GetValueAsync().ContinueWithOnMainThread(task => {
             if (task.IsFaulted)
             {
                 Debug.LogError(task.Exception);
                 _loadingText.text = "Could Not Connect to Network";


             }
             else if (task.IsCompleted)
             {

                 DataSnapshot snapshot = task.Result;
                 _loadingText.gameObject.SetActive(false);
                 GameManager.SetIndex(Convert.ToInt32(snapshot.ChildrenCount));
                 Debug.Log(Convert.ToInt32(snapshot.ChildrenCount));
                 for (int i = (_ScrollContent.transform.childCount>0? _ScrollContent.transform.childCount:0) ; i < Convert.ToInt32(snapshot.ChildrenCount); i++)
                 { 
                    GameObject _button = Instantiate(_ScrollButtonPrefab, new Vector3(0, 0, 0), _ScrollContent.transform.rotation, _ScrollContent.transform);
                     _button.GetComponent<RectTransform>().localPosition = new Vector3(_button.GetComponent<RectTransform>().localPosition.x, _button.GetComponent<RectTransform>().localPosition.y,0);
                     _button.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Painting " + (i+1).ToString();
                    _button.GetComponent<Button>().onClick.AddListener(delegate {RunAuto(i); });
                 }

             }
         });
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
