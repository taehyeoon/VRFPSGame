using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerController : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private ContinuousMoveProviderBase _continuousMoveProvider;
    [SerializeField] InputActionProperty leftControllerThumbstickClick;
    
    private void Awake()
    {
        _continuousMoveProvider = GetComponent<ContinuousMoveProviderBase>();
        _continuousMoveProvider.moveSpeed = runSpeed;
    }
    
    void Update()
    {
        if (IsRunning())
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        _continuousMoveProvider.moveSpeed = currentSpeed;
    }

    private bool IsRunning()
    {
        return leftControllerThumbstickClick.action.ReadValue<float>() > 0 || Input.GetKey(KeyCode.R);
    }
}
