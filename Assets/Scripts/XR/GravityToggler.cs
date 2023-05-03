using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityToggler : MonoBehaviour
{
    private ClimbingProvider climbingProvider;
    private ContinuousMoveProviderBase movePovider;

    private void Awake()
    {
        climbingProvider = GetComponentInParent<ClimbingProvider>();
        movePovider = GetComponentInParent<ContinuousMoveProviderBase>();
    }

    private void OnEnable()
    {
        climbingProvider.beginLocomotion += DisableGravity;
        climbingProvider.endLocomotion += EnableGravity;
    }

    private void OnDisable()
    {
        climbingProvider.beginLocomotion -= DisableGravity;
        climbingProvider.endLocomotion -= EnableGravity;
    }

    private void EnableGravity(LocomotionSystem locomotion) => ToggleGravity(true);
    private void DisableGravity(LocomotionSystem locomotion) => ToggleGravity(false);

    private void ToggleGravity(bool value)
    {
        movePovider.useGravity = value;
    }
}
