using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointController : MonoBehaviour
{
    public Action<int> tutorialPointAction;
    public AudioClip effectAudio;

    // Start is called before the first frame update
    void Start()
    {
        tutorialPointAction -= ActivateNextPoint;
        tutorialPointAction += ActivateNextPoint;
    }

    /*
     * audio �߻� ��, ���� Tutorial Point ��Ȱ��ȭ, ���� Tutorial Point Ȱ��ȭ
     */
    void ActivateNextPoint(int previousOrder)
    {
        // Audio Effect
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(effectAudio);

        // Point & Canvas invisible, visible
        GameObject currentPoint = transform.GetChild(previousOrder).gameObject;
        currentPoint.SetActive(false);
        currentPoint.GetComponent<TutorialPoint>().matchingCanvas.enabled = false;

        GameObject nextPoint = transform.GetChild(previousOrder + 1).gameObject;
        if(nextPoint == null)
        {
            Debug.Log($"There isn't {previousOrder + 1} order UI");
            return;
        }

        nextPoint.SetActive(true);
        nextPoint.GetComponent<TutorialPoint>().matchingCanvas.enabled = true;
    }
}
