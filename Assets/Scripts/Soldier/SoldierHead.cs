using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHead : MonoBehaviour
{
    private float _headShotMultiplier;
    private Soldier _soldier;
    private void Awake()
    {
        _soldier = transform.GetComponentInParent<Soldier>();
        if (_soldier != null)
        {
            _headShotMultiplier = _soldier.headShotMultiplier;
        }
        else
        {
            Debug.LogError("there is no soldier script in parent");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>(); 
        if (bullet != null)
        {
            Soldier soldier = transform.GetComponentInParent<Soldier>();
            if (soldier != null)
            {
                soldier.TakeDamage(bullet.damage * _headShotMultiplier);
            }
        }
    }
}
