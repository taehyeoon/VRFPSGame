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

    // If the target is not shot and killed during this time, it will disappear automatically
    public float targetLifeTime;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        state = ETarget.Stay;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        Destroy(gameObject, targetLifeTime);
        Debug.Log("target destroy after " + targetLifeTime + " s");
    }

    public void TakeDamage(float damageAmount)
    {
        // No damage if target is dead
        if(state == ETarget.Dead) return;
        
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
        _animator.SetTrigger("Die");
        ScoreController.kill();
    }
}