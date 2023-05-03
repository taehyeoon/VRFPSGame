using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public float damage;
    
    public void SetBulletData(float bulletDamage)
    {
        this.damage = bulletDamage;
    }

    private void OnCollisionEnter(Collision other)
    {
        Managers.Instance.audioManager.PlaySfx("pistolBulletImpactWall");
        Destroy(gameObject);
    }
}