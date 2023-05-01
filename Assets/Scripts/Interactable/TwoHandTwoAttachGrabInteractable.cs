using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class AttachTransforms
{
    public Transform rightTransform;
    public Transform leftTransform;
}

public class TwoHandTwoAttachGrabInteractable : XRGrabInteractable
{
    public AttachTransforms main;
    public AttachTransforms sub;

    public bool disableFirstCollider;

    [SerializeField]
    private bool alreadyMain = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left"))
        {
            if (alreadyMain)
            {
                attachTransform = main.leftTransform;
                alreadyMain = true;
            }
            else
                secondaryAttachTransform = sub.leftTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right"))
        {
            if (alreadyMain)
            {
                attachTransform = main.rightTransform;
                alreadyMain = true;
            }   
            else
                secondaryAttachTransform = sub.rightTransform;
        }

        if (disableFirstCollider == true)
            args.interactableObject.colliders[0].enabled = false;

        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {

        if (disableFirstCollider == true)
            args.interactableObject.colliders[0].enabled = true;

        secondaryAttachTransform = null;
        base.OnSelectExited(args);
    }

    /*public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(firstInteractorSelecting);

        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }*/
}
