using System;
using UnityEngine;
using NaughtyAttributes;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] string DebugSound;
    void Start()
    {
        Play("MainTheme");
    }
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    [Button]
    void DebugPlay()
    {

        Play(DebugSound);
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        Debug.Log(s.name + " playing");
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

}
