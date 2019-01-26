using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] audio = new Sound[10];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in audio)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(audio, sound => sound.Name == name);
        if (s == null)
            return;
        s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
