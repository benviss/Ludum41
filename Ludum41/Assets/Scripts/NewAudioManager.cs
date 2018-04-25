
using UnityEngine.Audio;
using UnityEngine;
using System;

public class NewAudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static NewAudioManager instance;

    // Use this for initialization
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Theme", transform, .7f, 1, true);
    }

    // Update is called once per frame
    public AudioSource Play(string name)
    {
        return Play(name, transform, 1f, 1f, false);
    }
    // Update is called once per frame
    public AudioSource Play(string name, bool loop)
    {
        return Play(name, transform, 1f, 1f, true);
    }
    public AudioSource Play(string name, Transform emitter)
    {
        return Play(name, emitter, 1f, 1f, false);
    }

    public AudioSource Play(string name, Transform emitter, float volume)
    {
        return Play(name, emitter, volume, 1f, false);
    }

    public AudioSource Play(string name, Transform emitter, float volume, float pitch, bool loop)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return null;
        }

        //Create an empty game object
        GameObject go = new GameObject("Audio: " + s.name);
        go.transform.position = emitter.position;
        go.transform.parent = emitter;

        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = s.clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = loop;
        source.Play();
        Destroy(go, s.clip.length);
        return source;
    }

    public AudioSource Play(string name, Vector3 point)
    {
        return Play(name, point, 1f, 1f);
    }

    public AudioSource Play(string name, Vector3 point, float volume)
    {
        return Play(name, point, volume, 1f);
    }

    public AudioSource Play(string name, Vector3 point, float volume, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return null;
        }

        //Create an empty game object
        GameObject go = new GameObject("Audio: " + s.name);
        go.transform.position = point;

        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = s.clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        Destroy(go, s.clip.length);
        return source;
    }
}
