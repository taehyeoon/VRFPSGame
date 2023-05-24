using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    
    private void Awake()
    {
        // for Unity editor test
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
