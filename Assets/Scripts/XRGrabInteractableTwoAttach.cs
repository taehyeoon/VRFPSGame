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
     * 1. 왼손, 오른손 구분
     * 2. 오브젝트 전체를 감싸는 collider 제거
     *      colliders[0]은 grab 판정 전용임. grab 이후엔 없어야 다른 손으로 interaction할 수 있음
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
