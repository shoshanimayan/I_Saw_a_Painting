using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    /////////////////////////
    // INSPECTOR VARIABLES //
    /////////////////////////
    [SerializeField]
    private AssetReference firstScene;
    [SerializeField]
    private TextMeshProUGUI _startText;

    /////////////////////////
    //  PRIVATE VARIABLES  //
    /////////////////////////
    private AsyncOperationHandle<SceneInstance> _handle;
    private bool _unloaded;

    ///////////////////////
    //  PRIVATE METHODS  //
    ///////////////////////
    private void Awake()
    {
        _startText.text = "Loading";
        Application.targetFrameRate = 90;
        if (Unity.XR.Oculus.Performance.TryGetDisplayRefreshRate(out var rate))
        {
            float newRate = 90f; // fallback to this value if the query fails.
            if (Unity.XR.Oculus.Performance.TryGetAvailableDisplayRefreshRates(out var rates))
            {
                newRate = rates.Max();
            }
            if (rate < newRate)
            {
                if (Unity.XR.Oculus.Performance.TrySetDisplayRefreshRate(newRate))
                {
                    Time.fixedDeltaTime = 1f / newRate;
                    Time.maximumDeltaTime = 1f / newRate;
                }
            }
        }
    }

    
    private void Start()
    {
        FirstLoad();
    }

    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            _handle = obj;
            _unloaded = false;
            StartCoroutine(ResetCamera());
            _startText.text = "Start";
            GameManager._loaded = true;
            GameManager.ToMenu();
        }
    }



    private void UnloadScene()
    {
        Debug.Log("unloading level");
        Debug.Log(_handle);
        Addressables.UnloadSceneAsync(_handle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
                Debug.Log("Successfully unloaded scene.");
            else
            {
                Debug.Log(op.Status.ToString());
            }
        };
    }

    //////////////////
    //  PUBLIC API  //
    /////////////////

    public void FirstLoad()
    {
        Addressables.LoadSceneAsync(firstScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;

    }

    public IEnumerator ResetCamera()
    {

        yield return new WaitForEndOfFrame();

        FindObjectOfType<XRRig>().transform.eulerAngles = new Vector3(0,180,0);
    }

    public void Load(AssetReference scene)
    {
        Debug.Log("loading level");
        if (!_unloaded)
        {
            _unloaded = true;
            UnloadScene();
        }
        Addressables.LoadSceneAsync(scene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;
    }

    
}