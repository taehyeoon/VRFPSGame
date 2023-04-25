using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    // AudioClip�� AudioManager�� ��������
    // order�� UIManager??
    private AudioSource _audioSource;
    public int order;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioSource.PlayOneShot(_audioSource.clip);
        transform.root.GetComponent<TutorialPointController>().tutorialPointAction.Invoke(order);
    }
}
