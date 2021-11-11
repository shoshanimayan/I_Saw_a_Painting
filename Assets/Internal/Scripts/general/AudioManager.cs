using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] AudioClip[] _audioClips;
    private AudioSource _as;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
    }

    public void PlayClip(string name)
    {
        foreach (AudioClip clip in _audioClips)
        {
            if (clip.name == name)
            {
                _as.PlayOneShot(clip);
            }
        }
    
    }
}
