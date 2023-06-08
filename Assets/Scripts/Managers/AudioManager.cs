using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public AudioSource musicSource, sfxSource, pistolSource, bulletSource, 
        metalSource, concreteSource, woodSource;

    public Sound[] musicSounds, sfxSounds, pistolSounds, bulletSounds, footstepSounds;

    public void PlayMusic(string sName)
    {
        Sound s = Array.Find(musicSounds, x => x.name == sName);
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
    
    public void PlaySfx(string sName)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == sName);
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

    public void PlayBullet(string sName)
    {
        Sound s = Array.Find(bulletSounds, x => x.name == sName);
        if (s == null)
        {
            Debug.Log("SFX Sound not found");
        }
        else
        {
            bulletSource.PlayOneShot(s.clip);
        }
    }

    public void StartMetal()
    {
        metalSource.Play();
    }
    public void StartConcrete()
    {
        concreteSource.Play();
    }
    public void StartWood()
    {
        woodSource.Play();
    }

    public void StopFootStep()
    {
        metalSource.Stop();
        concreteSource.Stop();
        woodSource.Stop();
    }
}
