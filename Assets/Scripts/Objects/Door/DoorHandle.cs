using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class DoorHandle : MonoBehaviour
{
    private XRGrabInteractable interactable;
    private AudioSource audioSource;

    public Collider detectCollider;
    public Rigidbody doorRigidbody;
    public AudioClip grabAudio;
    public AudioClip openAudio;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        interactable.selectEntered.AddListener(DisableKinematic);
        
        interactable.selectExited.AddListener(EnableCollider);
    }

    private void DisableKinematic(SelectEnterEventArgs args)
    {
        audioSource.PlayOneShot(grabAudio);
        detectCollider.enabled = false;
        doorRigidbody.isKinematic = false;
    }

    private void EnableCollider(SelectExitEventArgs args)
    {
        audioSource.PlayOneShot(openAudio);
        detectCollider.enabled = true;
    }
}
