using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnableKinematic : MonoBehaviour
{
    public AudioClip closedAudio;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            audioSource.PlayOneShot(closedAudio);
            collision.rigidbody.isKinematic = true;
        }
    }
}
