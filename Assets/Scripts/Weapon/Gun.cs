using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Gun : MonoBehaviour
{
    [Header("Gun data")]
    protected bool isMagazineAttached;
    // Unable to fire gun when holding slider
    protected bool isSliderReleased;
    // If the slider is pulled by slidePullAmount distance, it is recognized as reloaded
    protected abstract float SlidePullAmount { get; set; }

    [Header("Bullet data")]
    [SerializeField] protected float damage;
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int magazineSize;
    [SerializeField] protected float bulletSpeed;

    [Header("Position")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Transform shellPoint;
    [SerializeField] protected Transform slider;
    protected float initialSliderZPos;
    
    [Header("Prefab")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject shellPrefab;
    [SerializeField] protected GameObject magazineSocket;

    protected int fireRate;
    protected int range;
    protected float accuracy;
    protected float effectRange;
    protected float recoil;

    protected void Awake()
    {
        isSliderReleased = true;
    }

    protected void Update()
    {
        isMagazineAttached = magazineSocket.activeSelf;
    }

    public abstract void OnActivated();
    protected abstract void Fire();
    protected abstract void Reload();
    
    public abstract void CheckIsLoaded();
    public abstract void MarkInitialSliderZPosition();

    public void SetIsSliderReleased(bool value)
    {
        isSliderReleased = value;
    }

}
