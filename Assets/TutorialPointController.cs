using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPointController : MonoBehaviour
{
    public Action<int> tutorialPointAction;

    // Start is called before the first frame update
    void Start()
    {
        tutorialPointAction -= ActivateNextPoint;
        tutorialPointAction += ActivateNextPoint;
    }

    void ActivateNextPoint(int previousOrder)
    {
        transform.GetChild(previousOrder).gameObject.SetActive(false);

        GameObject nextPoint = transform.GetChild(previousOrder + 1).gameObject;
        if (nextPoint != null)
            nextPoint.SetActive(true);
    }
}
