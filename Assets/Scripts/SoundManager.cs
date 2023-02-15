using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        { 
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumn;
            s.source.pitch = s.pitch;
            s.source.loop= s.loop;
        }
    }
    private void Start()
    {
        Play("bg");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
