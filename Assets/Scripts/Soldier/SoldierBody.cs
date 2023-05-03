using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBody : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>(); 
        if (bullet != null)
        {
            Soldier soldier = transform.GetComponentInParent<Soldier>();
            if (soldier != null)
            {
                soldier.TakeDamage(bullet.damage);
            }
        }
    }
}
