using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private Animator _animator;
    
    public float headShotMultiplier;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        Debug.Log($"take damage  {damageAmount}");
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.CrossFade("Die", 0.2f);
    }
}