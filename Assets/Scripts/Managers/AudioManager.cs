using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public AudioSource musicSource, sfxSource, pistolSource;

    public Sound[] musicSounds, sfxSounds, pistolSounds;

    // [Header("pistol")] 
    // public AudioClip fire_pistol;
    // public AudioClip dry_fire_pistol;
    // public AudioClip reload_pistol;
    //
    void Awake()
    {
        // InitSound();
    }

    // private void InitSound()
    // {
    //     string pistol_root = "Audios/Pistol/";
    //     fire_pistol = Resources.Load<AudioClip>(pistol_root + fire_pistol);
    //     dry_fire_pistol = Resources.Load<AudioClip>(pistol_root + dry_fire_pistol);
    //     reload_pistol = Resources.Load<AudioClip>(pistol_root + reload_pistol);
    // }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("music Sound not found");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
    
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("SFX Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayPistol(string sName)
    {
        Sound s = Array.Find(pistolSounds, x => x.name == sName);
        if (s == null)
        {
            Debug.Log("SFX Sound not found");
        }
        else
        {
            pistolSource.PlayOneShot(s.clip);
        }
    }
}
