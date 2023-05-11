using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject shellPrefab;
    [SerializeField] protected Transform shellPoint;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int magazineSize;
    protected int fireRate;
    protected int range;
    protected float accuracy;
    [SerializeField] protected float bulletSpeed;
    [SerializeField] protected float damage;
    protected float effectRange;
    protected float recoil;

    [SerializeField] protected AudioClip fireAudio;
    [SerializeField] protected AudioClip bulletHitWallAudio;
    protected AudioSource audioSource;
    
    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public abstract void OnActivated();
    protected abstract void FireBulletOnActivate();
    protected abstract void Fire();
}
