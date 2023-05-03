using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VelocityContainer : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty velocityInput;
    
    public Vector3 Velocity => velocityInput.action.ReadValue<Vector3>();
}
