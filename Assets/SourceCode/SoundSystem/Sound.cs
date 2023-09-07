using UnityEngine.Audio;
using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioSource source;
    public AudioClip clip;
    [SerializeField, Range(0, 1)] public float volume = 0.5f;
    [SerializeField,Range(-3,3)]public float pitch = 1;
    public bool loop;
    
}