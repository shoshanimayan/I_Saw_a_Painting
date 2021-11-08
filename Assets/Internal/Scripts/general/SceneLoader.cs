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
    [SerializeField]
    private AssetReference firstScene;
    [SerializeField]
    private Image _loadingImage;
    [SerializeField]
    private GameObject _canvasLoading;
    private AsyncOperationHandle<SceneInstance> _handle;
    private bool _unloaded;
    private bool _loading;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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

    private void Update()
    {
        if (_loading)
        {
            _loadingImage.transform.Rotate(0, 0, 50);
        }
    }
    void Start()
    {
        FirstLoad();
    }

    public void FirstLoad()
    {
        _loading = true;
        Addressables.LoadSceneAsync(firstScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadCompleted;

    }

    public IEnumerator ResetCamera()
    {

        yield return new WaitForEndOfFrame();

        FindObjectOfType<XRRig>().transform.eulerAngles = Vector3.zero;
    }

    // Start is called before the first frame update
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

    private void SceneLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            _loading = false;
            Debug.Log("Successfully loaded scene.");
            _handle = obj;
            _unloaded = false;
            StartCoroutine(ResetCamera());
            _canvasLoading.SetActive(false);
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
}