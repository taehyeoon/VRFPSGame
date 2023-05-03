using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// public enum EBulletType
// {
//     pistol,
//     Rifle,
// }

public class Bullet : MonoBehaviour
{
    // public EBulletType bulletType;
    public float damage;
    public AudioClip bulletHitWallAudio;
    private AudioSource _audioSource;
    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void setBulletData(float damage, AudioClip bulletHitWallAudio)
    {
        this.damage = damage;
        this.bulletHitWallAudio = bulletHitWallAudio;
    }

    private void OnCollisionEnter(Collision other)
    {
        _audioSource.PlayOneShot(bulletHitWallAudio);
    }
}