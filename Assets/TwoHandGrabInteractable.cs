using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum TwoHandRotationType { None, First, Second };

public class TwoHandGrabInteractable : XRGrabInteractableTwoAttach
{
    public List<XRGrabInteractable> secondHandGrabPoints= new List<XRGrabInteractable>();
    
    public TwoHandRotationType rotationType;
    public bool snapToSecondHand = true;

    private XRBaseInteractor secondInteractor;
    private Quaternion attachInitialRotation;
    private Quaternion initialRotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in secondHandGrabPoints)
        {
            item.selectEntered.AddListener(OnSecondHandGrab);
            item.selectExited.AddListener(OnSecondHandRelease);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(secondInteractor && selectingInteractor)
        {
            // Rotate
            if(snapToSecondHand)
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
            else
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation() * initialRotationOffset;
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation = new Quaternion();
        Vector3 forward = secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position;
        switch (rotationType)
        {
            case TwoHandRotationType.None:
                targetRotation = Quaternion.LookRotation(forward);
                break;
            case TwoHandRotationType.First:
                targetRotation = Quaternion.LookRotation(forward, selectingInteractor.attachTransform.up);
                break;
            case TwoHandRotationType.Second:
                targetRotation = Quaternion.LookRotation(forward, secondInteractor.attachTransform.up);
                break;
        }

        return targetRotation;
    }

    public void OnSecondHandGrab(SelectEnterEventArgs interactor)
    {
        Debug.Log("Second Hand Grab");
        secondInteractor = interactor.interactorObject as XRBaseInteractor;

        initialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
    }

    public void OnSecondHandRelease(SelectExitEventArgs interactor)
    {
        Debug.Log("Second Hand Release");
        secondInteractor = null;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("First Hand Grab");
        base.OnSelectEntered(args);

        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        attachInitialRotation = interactor.attachTransform.localRotation;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("First Hand Release");
        base.OnSelectExited(args);
        secondInteractor = null;

        XRBaseInteractor interactor = args.interactorObject as XRBaseInteractor;
        interactor.attachTransform.localRotation = attachInitialRotation;
    }

    /*
     * IsSelectableBy
     * 이미 어느 한 interactor에 의해 grabbed 상태면, 더이상 select하지 못하도록 막음
     */
    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        bool isAlreadyGrabbed = isSelected && !interactor.Equals(firstInteractorSelecting);

        return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
    }



}
