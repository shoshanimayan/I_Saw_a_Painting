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
    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////

    [SerializeField] private GameObject _menus;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private TextMeshProUGUI _titleText;

    [SerializeField] private GameObject _ScrollContent;
    [SerializeField] private GameObject _ScrollButtonPrefab;
    [SerializeField] InputActionReference controllerActionGrip;

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////
    private void Awake()
    {
        controllerActionGrip.action.started += EndPlayManual;
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
                 _loadingText.text = "Could Not Connect to Network";


             }
             else if (task.IsCompleted)
             {

                 DataSnapshot snapshot = task.Result;
                 _loadingText.gameObject.SetActive(false);
                 GameManager.SetIndex(Convert.ToInt32(snapshot.ChildrenCount));
                 List<int> nums = new List<int>();
                 for (int i = (_ScrollContent.transform.childCount>0? _ScrollContent.transform.childCount:0) ; i < Convert.ToInt32(snapshot.ChildrenCount); i++)
                 {
                     nums.Add(i);
                    
                 }

                 foreach (int i in nums.ToArray())
                 {
                     GameObject _button = Instantiate(_ScrollButtonPrefab, new Vector3(0, 0, 0), _ScrollContent.transform.rotation, _ScrollContent.transform);
                     _button.GetComponent<RectTransform>().localPosition = new Vector3(_button.GetComponent<RectTransform>().localPosition.x, _button.GetComponent<RectTransform>().localPosition.y, 0);
                     _button.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Painting " + (i + 1).ToString();
                     _button.GetComponent<Button>().onClick.AddListener(delegate { RunAuto(i); });
                 }

             }
         });
    }


    //////////////////
    //  PUBLIC API  //
    /////////////////

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
        _titleText.fontSize = 7;
        _titleText.text = "Uploaded Painting " + title;
    }

    public void RunAuto(int key)
    {
        Debug.Log(key);
        GameManager.PlayRecord(key);
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
