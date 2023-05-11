using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointController : MonoBehaviour
{
    public Action<int> tutorialPointAction;
    public AudioClip effectAudio;

    [SerializeField]
    private int childCount;

    // Start is called before the first frame update
    void Awake()
    {
        tutorialPointAction -= ActivateNextPoint;
        tutorialPointAction += ActivateNextPoint;

        InitTutorialPoints();
    }

    /*
     * audio 발생 후, 이전 Tutorial Point 비활성화, 다음 Tutorial Point 활성화
     */
    void ActivateNextPoint(int previousOrder)
    {
        // Audio Effect
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(effectAudio);

        // Point & Canvas invisible, visible
        TutorialPoint currentPoint = transform.GetChild(previousOrder).GetComponent<TutorialPoint>();
        currentPoint.gameObject.SetActive(false);
        if (currentPoint.matchingCanvas != null)
            currentPoint.matchingCanvas.enabled = false;

        if (childCount <= previousOrder + 1)
            return;

        Transform nextPoint = transform.GetChild(previousOrder + 1);
        if(nextPoint == null)
        {
            Debug.Log($"There isn't {previousOrder + 1} order UI");
            return;
        }

        nextPoint.gameObject.SetActive(true);

        Canvas canvas = nextPoint.GetComponent<TutorialPoint>().matchingCanvas;
        if(canvas != null)
            canvas.enabled = true;
    }

    void InitTutorialPoints()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            TutorialPoint tutorialPoint = transform.GetChild(i).GetComponent<TutorialPoint>();

            tutorialPoint.order = i;
            if(tutorialPoint.matchingCanvas != null)
                tutorialPoint.matchingCanvas.enabled = false;
            tutorialPoint.gameObject.SetActive(false);
        }

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<TutorialPoint>().matchingCanvas.enabled = true;

        childCount = transform.childCount;
    }
}
