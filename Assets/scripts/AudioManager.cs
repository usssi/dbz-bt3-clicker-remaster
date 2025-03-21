using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    /*
    FindObjectOfType<AudioManager>().Play("",1f);
    */

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }

        //print("audio manager woke up");
        FindObjectOfType<AudioManager>().Play("ruido", 1f);
    }

    public void Play(string name, float pitchTone)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = pitchTone;


        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
        //print("audio manager played" + name);
    }

    public void OnButtonSelect()
    {
        FindObjectOfType<AudioManager>().Play("buttonSelect", 1f);
    }
}
