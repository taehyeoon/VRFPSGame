using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ETarget
{
    Stay,
    Move,
    Dead,
}
public class Soldier : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private Animator _animator;

    public ETarget state;
    public float headShotMultiplier;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        state = ETarget.Stay;
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
        state = ETarget.Dead;
        _animator.CrossFade("Die", 0.2f);
    }
}