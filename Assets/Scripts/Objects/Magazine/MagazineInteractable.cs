using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineInteractable : XRGrabInteractable
{
    [SerializeField]
    private bool isAlreadInSocket;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject is XRSocketInteractor)
        {
            isAlreadInSocket = true;
        }

        base.OnSelectEntered(args);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if(interactor is XRSocketInteractor)
            return base.IsSelectableBy(interactor);
        else
            return !isAlreadInSocket && base.IsSelectableBy(interactor);
    }
}
