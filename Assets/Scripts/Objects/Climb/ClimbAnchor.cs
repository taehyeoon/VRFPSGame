using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbAnchor : XRBaseInteractable
{
    [SerializeField]
    private ClimbingProvider climbingProvider;
    private AudioSource audioSource;

    public AudioClip audioClip;

    protected override void Awake()
    {
        base.Awake();
        FindClimbingProvider();

        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
            gameObject.AddComponent<AudioSource>();
    }

    private void FindClimbingProvider()
    {
        if (!climbingProvider)
        {
            climbingProvider = FindObjectOfType<ClimbingProvider>();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        TryAdd(args.interactorObject);

        audioSource.PlayOneShot(audioClip);
    }

    private void TryAdd(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer container))
            climbingProvider.AddProvider(container);
    }

    private void TryRemove(IXRSelectInteractor interactor)
    {
        if (interactor.transform.TryGetComponent(out VelocityContainer container))
            climbingProvider.RemoveProvider(container);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        TryRemove(args.interactorObject);
        
    }

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        return base.IsHoverableBy(interactor) && interactor is XRDirectInteractor;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return base.IsSelectableBy(interactor) && interactor is XRDirectInteractor;
    }

}
