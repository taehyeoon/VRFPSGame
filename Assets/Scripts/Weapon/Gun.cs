using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

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
    [SerializeField] protected float range;
    
    [Header("Position")]
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Transform shellPoint;
    [SerializeField] protected Transform slider;
    protected float initialSliderZPos;
    
    [Header("Prefab")]
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject shellPrefab;
    [SerializeField] protected GameObject magazineSocket;

    [Header("Controller")]
    [SerializeField] protected InputActionProperty unloadMagazine;

    
    protected int fireRate;
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

        if (isMagazineAttached)
        {
            if (unloadMagazine.action.ReadValue<float>() >= 0.8 || Input.GetKey(KeyCode.T))
                UnloadMagzine();
        }
    }

    public abstract void OnActivated();
    protected abstract void Fire();
    protected abstract void Reload();
    
    public abstract void CheckIsLoaded();
    public abstract void MarkInitialSliderZPosition();

    // find gun's magazine, enabled false
    protected void UnloadMagzine()
    {
        MagazineInteractable magazine = GetComponentInChildren<MagazineInteractable>();
        magazine.enabled = false;
    }

    public void SetIsSliderReleased(bool value)
    {
        isSliderReleased = value;
    }

}
