using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] 
    AudioClip _gameMusic;

    AudioSource _audioSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public void PlayGameMusic()
    {
        _audioSource.clip = _gameMusic;
        _audioSource.volume = .5f;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }
}
