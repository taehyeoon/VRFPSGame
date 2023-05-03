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

    public override void OnActivated()
    {
        if (currentAmmo > 0)
        {
            BlowEmptyShell();
        }
        FireBulletOnActivate();
    }

    private void BlowEmptyShell()
    {
        GameObject shell = Instantiate(shellPrefab, shellPoint.position, Quaternion.identity);
        shell.AddComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Impulse);
        /*
        float magnitude = 3f;
        Vector3 pos = shell.transform.position;
        pos.z += 1f;
        rb.AddForceAtPosition(new Vector3(1,0,0)*magnitude, pos);
        */
        SphereCollider col =  shell.AddComponent<SphereCollider>();
        col.radius = 0.01f;
        Destroy(shell, 3f);
    }

    protected override void FireBulletOnActivate()
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
        rb.freezeRotation = false;
        rb.AddForce((-transform.forward).normalized * bulletSpeed, ForceMode.Impulse);
        
        Managers.Instance.audioManager.PlaySfx("firePistol");
    }
}
