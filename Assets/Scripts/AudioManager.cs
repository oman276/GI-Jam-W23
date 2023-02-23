using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public string winner = "NONE";
    public string loser = "NONE";

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
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

        Play("theme");
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            print("Sound " + name + "not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            print("Sound " + name + " not found");
            return;
        }
        /*
        if (s.source)
        {
            print("source found for " + name);
        }
        else {
            print("source not found for " + name);
        }
        */
        s.source.Stop();
    }
    public void muteSound(bool muted)
    {
        if (muted)
        {
             foreach (Sound s in sounds) {
			s.source.volume = 0;
			
		 }
        } else {
             foreach (Sound s in sounds) {
		 	s.source.volume = 0.3f;
			s.volume = 0.3f;
             }
        }
    }
}