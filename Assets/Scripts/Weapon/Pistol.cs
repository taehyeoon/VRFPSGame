using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : Gun
{
    private void Awake()
    {
        base.Awake();
    }

    public override void FireBulletOnActivate()
    {
        if (currentAmmo > 0)
        {
            Fire();
            currentAmmo--;
        }
        else
        {
            if (maxAmmo >= magazineSize)
            {
                currentAmmo = magazineSize;
                maxAmmo -= currentAmmo;
                Fire();
            }
            else if(maxAmmo > 0)
            {
                currentAmmo = maxAmmo;
                maxAmmo = 0;
                Fire();
            }
            else
            {
                Debug.Log("outOfAmmo");
            }
        }
    }

    protected override void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        bullet.AddComponent<SphereCollider>();
        bullet.AddComponent<Bullet>().SetBulletData(damage);
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.AddForce((-transform.forward).normalized * bulletSpeed, ForceMode.Impulse);
        
        Managers.Instance.audioManager.PlaySfx("firePistol");
    }
}
