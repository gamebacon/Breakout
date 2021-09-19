using System;
using UnityEngine;

namespace util
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        [Range(0f,1f)] public float volume = 1;
        [Range(0f,3f)] public float pitch = 1;
        [HideInInspector] public AudioSource audioSource;
    }
}