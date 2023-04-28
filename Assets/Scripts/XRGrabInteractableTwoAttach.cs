using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttachTransform;
    public Transform rightAttachTransform;

    /*
     * OnSelectEneterd
     * 1. �޼�, ������ ����
     * 2. ������Ʈ ��ü�� ���δ� collider ����
     *      colliders[0]�� grab ���� ������. grab ���Ŀ� ����� �ٸ� ������ interaction�� �� ����
     */
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left"))
        {
            attachTransform = leftAttachTransform;
        }
        else if(args.interactorObject.transform.CompareTag("Right"))
        {
            attachTransform = rightAttachTransform;
        }

        args.interactableObject.colliders[0].enabled = false;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        args.interactableObject.colliders[0].enabled = true;
        base.OnSelectExited(args);
    }
}
