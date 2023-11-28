using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public AudioMixerGroup musicMixer;
    public Sound[] sounds;

    public static AudioManager instance;
    

    void Awake()
    {
        // if the object doesnt exist as a static instance plz make one
        // otherwise plz destroy cause we dont wan 2 AudioManagers :(
        if (instance == null)
        { 
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            if (s.name != "Music")
            {
                s.source.outputAudioMixerGroup = mixer;
            } else
            {
                s.source.outputAudioMixerGroup = musicMixer;
            }
            

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }    
    }

    public void PlayButton()
    {
        Play("Button");
    }

    void Start()
    {
        Play("Music");
        StartCoroutine(FadeIn("Music"));
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: "+ name + " not found!");
            return;
            
        }
        s.source.Play();
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: "+ name + " not found!");
            return;
            
        }
        s.source.Stop();
    }

    public bool isPlaying (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: "+ name + " not found!");
            return false;
            
        }
        return(s.source.isPlaying);
    }

    public void ModulatePitch (string name, float amountToLower = 0.1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: "+ name + " not found!");
            return;
        }
        if (s.source.pitch > Math.Abs(amountToLower))
        {
            s.source.pitch += amountToLower;
        }
    }

    public void PitchBackToNormal (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: "+ name + " not found!");
            return;
        }
        s.source.pitch = 1.0f;
    }

    public void PitchBackToNormalAll ()
    {
        foreach (Sound s in sounds)
        {
            if (s == null)
            {
                Debug.LogWarning("Sound: "+ name + " not found!");
                return;
            }
        s.source.pitch = 1.0f;
        }
    }



    IEnumerator FadeIn(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.volume = 0;
        s.source.volume = 0;
        float speed = 0.001f;   

        for (float i = 0; i < 0.30f; i += speed)
        {
            s.volume = i;
            s.source.volume = i;
            yield return null;
        }
    }


}
