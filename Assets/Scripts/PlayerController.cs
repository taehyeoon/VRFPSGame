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

    public LayerMask floorLayer;

    public Vector3 prePos;
    public bool isPlayerStop;
    private void Awake()
    {
        _continuousMoveProvider = GetComponent<ContinuousMoveProviderBase>();
        _continuousMoveProvider.moveSpeed = walkSpeed;
        
        floorLayer = LayerMask.NameToLayer("Floor");
    }
    
    void Update()
    {
        if (prePos == transform.position) 
            isPlayerStop = true;
        else
            isPlayerStop = false;
        prePos = transform.position;


        
        if (IsRunning())
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        _continuousMoveProvider.moveSpeed = currentSpeed;


        if (isPlayerStop)
        {
            Managers.Instance.audioManager.StopFootStep();
        }
        else
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), Vector3.down);

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, 1.0f, LayerMask.GetMask("Floor")))
            {
                if (hit.collider.CompareTag("Metal") && !Managers.Instance.audioManager.metalSource.isPlaying)
                {
                    Managers.Instance.audioManager.StopFootStep();
                    Managers.Instance.audioManager.StartMetal();
                }
                else if (hit.collider.CompareTag("Wood") && !Managers.Instance.audioManager.woodSource.isPlaying)
                {
                    Managers.Instance.audioManager.StopFootStep();
                    Managers.Instance.audioManager.StartWood();
                }
                else if (hit.collider.CompareTag("Concrete") && !Managers.Instance.audioManager.concreteSource.isPlaying)
                {
                    Managers.Instance.audioManager.StopFootStep();
                    Managers.Instance.audioManager.StartConcrete();
                }
            }
        }

    }

    private bool IsRunning()
    {
        return leftControllerThumbstickClick.action.ReadValue<float>() > 0 || Input.GetKey(KeyCode.R);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     var position = transform.position;
    //     Gizmos.DrawLine(position + Vector3.up, position + (-Vector3.up) * 3f);
    // }
    
}
