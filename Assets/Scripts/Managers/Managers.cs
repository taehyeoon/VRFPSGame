using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Managers : MonoBehaviour
{
    public static Managers Instance;
    public GameManager gameManager;
    public AudioManager audioManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameManager.player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
}
