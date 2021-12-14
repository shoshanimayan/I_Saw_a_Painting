using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] AudioClip[] _audioClips;
    private AudioSource _as;
    private AudioSource _asCamera;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
        _asCamera = Camera.main.gameObject.GetComponent<AudioSource>();
    }

    public void PlayClip(string name)
    {
        foreach (AudioClip clip in _audioClips)
        {
            if (clip.name == name)
            {
                _asCamera.PlayOneShot(clip);
            }
        }
    
    }

    public void PlayMainTheme()
    {
        _as.Play();
    }
    public void StopMainTheme()
    {
        _as.Stop();
    }
}
