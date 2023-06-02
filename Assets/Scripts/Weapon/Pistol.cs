using System;
using UnityEngine;

public class Pistol : Gun
{
    protected override float SlidePullAmount { get; set; }

    // Check which hand is holding the gun
    private XRGrabInteractableTwoAttach grabInteractableTwoAttach;
    private SetActiveGrabHand currentHand;
    private new void Awake()
    {
        base.Awake();
        SlidePullAmount = 0.02f;
        animator = GetComponent<Animator>();
        currentHand = GetComponent<SetActiveGrabHand>();
        animator.enabled = false;
    }


    // Executed when the grab button on the controller holding the gun is pressed
    public override void OnActivated()
    {
        if (!isMagazineAttached)
        {
            // Tick sound
            Managers.Instance.audioManager.PlayPistol("dry_fire_pistol");
            Debug.Log("Join the magazine");
            return;
        }

        if (!isSliderReleased)
        {
            Debug.Log("Release the slider to fire");
            return;
        }

        // Fire
        if(currentAmmo > 0)
        {
            Fire();
            BlowEmptyShell();
            currentAmmo -= 1;
        }
        else
        {
            // tick sound
            Managers.Instance.audioManager.PlayPistol("dry_fire_pistol");
            Debug.Log("Reload to fire");
        }



    }

    // Discharge of the empty shell
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

    protected override void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        bullet.GetComponent<Bullet>().SetBulletData(damage, range, firePoint.position);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce((-transform.forward).normalized * bulletSpeed, ForceMode.Impulse);
        
        Managers.Instance.audioManager.PlayPistol("fire_pistol");
        
        animator.SetTrigger("MoveSlider");
        Debug.Log(currentHand.CurrentHand.ToString());
        
        if(currentHand.CurrentHand == HandData.HandModelType.Left)
            Managers.Instance.gameManager.player.GetComponent<Animator>().SetTrigger("LeftFire");
        else if(currentHand.CurrentHand == HandData.HandModelType.Right)
            Managers.Instance.gameManager.player.GetComponent<Animator>().SetTrigger("RightFire");

    }

    // If the slider moves by "SlidePullAmount", it is recognized as reloaded
    public override void CheckIsLoaded()
    {
        if (initialSliderZPos != 0 && initialSliderZPos - slider.transform.localPosition.z > SlidePullAmount)
        {
            Reload();
            initialSliderZPos = 0;
        }
    }

    public override void MarkInitialSliderZPosition()
    {
        initialSliderZPos = slider.transform.localPosition.z;
    }

    protected override void Reload()
    {
        // Calculate the number of ammunition to reload
        int reloadAmmoCount = Math.Min(magazineSize - currentAmmo, maxAmmo); 
        maxAmmo -= reloadAmmoCount;
        
        // Increase the current number of ammunition by the calculated value
        currentAmmo += reloadAmmoCount;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(firePoint.position, firePoint.position + (-firePoint.forward).normalized * range);
    }
}
