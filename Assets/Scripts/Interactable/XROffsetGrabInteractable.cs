using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;

    private Gun gunScript;
    // Coroutine function to ensure that the slider is pulled enough
    private Coroutine sliderCoroutine;
    
    void Start()
    {
        if(!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offset Grab Pivot");
            attachPoint.transform.SetParent(transform, false);  // local position (0, 0, 0)���� ����
            attachTransform = attachPoint.transform;
        }
        else
        {
            initialLocalPos = attachTransform.localPosition;
            initialLocalRot = attachTransform.localRotation;
        }
        
        Transform grandParentTransform = transform.parent.parent;
        gunScript = grandParentTransform.GetComponentInParent<Gun>();
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        if (args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            attachTransform.localPosition = initialLocalPos;
            attachTransform.rotation = initialLocalRot;
        }
        
        Managers.Instance.audioManager.PlayPistol("reload_pistol");
        
        // Turn off slider animation when you hold the slider
        gunScript.animator.enabled = false;
        gunScript.MarkInitialSliderZPosition();
        gunScript.SetIsSliderReleased(false);
        
        sliderCoroutine = StartCoroutine(CheckSliderPulled());
    }

    private IEnumerator CheckSliderPulled()
    {
        while (true)
        {
            gunScript.CheckIsLoaded();
            yield return null; 
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        gunScript.SetIsSliderReleased(true);
        
        // Animated action when you release the slider
        gunScript.animator.enabled = true;
        if (sliderCoroutine != null)
        {
            // Stop calculating the slider's position if you release your hand from the slider
            StopCoroutine(sliderCoroutine);
            sliderCoroutine = null;
        }
    }
}
