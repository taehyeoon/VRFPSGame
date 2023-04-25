using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    // AudioClip은 AudioManager로 가져오기
    // order도 UIManager??
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
