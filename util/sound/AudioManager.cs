using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using util;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    private static AudioManager instance;
    
    void Start()
    {
        instance = this;
        
        foreach(Sound sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch= sound.pitch;
        }
        
    }
    
    public static void Play(string soundName)
    {
        Sound sound = getSound(soundName);
        sound.audioSource.Play();
    }
    
    public static void Play(string soundName, float pitch)
    {
        Sound sound = getSound(soundName);
        sound.audioSource.pitch = pitch;
        sound.audioSource.Play();
    }
    
    public static Sound getSound(String name)
    {
        return Array.Find(instance.sounds, sound => sound.clip.name == name);
    }
    
    
}
