using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private AudioSource _Source;
    
    [SerializeField]
    private AudioClip _ButtonSound;
    [SerializeField]
    private AudioClip _RareSound;
    [SerializeField]
    private AudioClip _EpicSound;

    private static SoundManager _SoundManager;

    public static SoundManager Instance
    {
        get
        {
            if (!_SoundManager) _SoundManager = FindObjectOfType<SoundManager>();
            return _SoundManager;
        }
    }

    void Awake()
    {
        _Source = GetComponent<AudioSource>();
    }

    public void PlayButtonClicked()
    {
        _Source.clip = _ButtonSound;
        _Source.Play();
    }

    public void PlayGotRare()
    {
        _Source.clip = _RareSound;
        _Source.Play();
    }

    public void PlayGotEpic()
    {
        _Source.clip = _EpicSound;
        _Source.Play();
    }
}
