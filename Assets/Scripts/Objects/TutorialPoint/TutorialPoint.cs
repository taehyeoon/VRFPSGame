using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    // order�� UIManager??
    public int order;
    public Canvas matchingCanvas;

    private void OnTriggerEnter(Collider other)
    {
        transform.root.GetComponent<TutorialPointController>().tutorialPointAction.Invoke(order);
    }
}
