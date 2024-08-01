using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JetBrains.Annotations;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayMusic(string name)
    {
        if (musicSource.isPlaying)
        {
            Debug.Log("should stop playing music ");    
            musicSource.Stop();
        }

        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        Debug.Log("stop playing musics");
        //musicSource.Pause();
        //musicSource.Stop();
        musicSource.clip = null;
        musicSource.Stop();

        Debug.Log(musicSource.isPlaying);

    }

    public void PlaySFX(string name)
    { 
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
            return;
        }

        if (sfxSource.isPlaying && sfxSource.clip == s.clip)
            return;

        sfxSource.PlayOneShot(s.clip);
        sfxSource.clip = s.clip;

    }
}
