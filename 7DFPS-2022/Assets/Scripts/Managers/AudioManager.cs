using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    }
    [Range(0f, 1f)]
    public float masterVolume;
    [Range(0f, 1f)]
    public float sfxVolume;
    [Range(0f, 1f)]
    public float musicVolume;
    public Sound[] sounds;

    public AudioSource Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return null;
        }
        s.source.volume = s.volume;
        if (s.type == 0)
        {
            s.source.volume = s.source.volume * sfxVolume;
        }
        if (s.type == 1)
        {
            s.source.volume = s.source.volume * musicVolume;
        }
        s.source.volume = s.source.volume * masterVolume;
        s.source.Play();
        return s.source;

    }
    public void PlayAtPositition(string name, Transform t, bool isPlayer)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found.");
            return;
        }
        s.source.volume = s.volume;
        if (s.type == 0)
        {
            s.source.volume = s.source.volume * sfxVolume;
        }
        if (s.type == 1)
        {
            s.source.volume = s.source.volume * musicVolume;
        }
        s.source.volume = s.source.volume * masterVolume;

        AudioSource.PlayClipAtPoint(s.source.clip, t.position, s.source.volume);

    }
    public void PlayAtPositition(string name, Transform t)
    {
        PlayAtPositition(name, t, false);
    }

    public void ChangeMasterVolume(float value)
    {
        PlayerPrefs.SetFloat("master_volume", value);
        masterVolume = value;
        AdjustCurrentSounds();
    }
    public void ChangeSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("sfx_volume", value);
        sfxVolume = value;
        AdjustCurrentSounds();
    }
    public void ChangeMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("music_volume", value);
        musicVolume = value;
        AdjustCurrentSounds();


    }
    public void AdjustCurrentSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
            if (s.type == 0)
            {
                s.source.volume = s.source.volume * sfxVolume;
            }
            if (s.type == 1)
            {
                //Debug.LogWarning(s.source.volume);
                s.source.volume = s.source.volume * musicVolume;
            }
            s.source.volume = s.source.volume * masterVolume;
        }
    }

    private void Start()
    {
        Play("MainTheme");
    }

}
