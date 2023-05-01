using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPoint : MonoBehaviour
{
    // orderµµ UIManager??
    public int order;
    public Canvas matchingCanvas;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        transform.root.GetComponent<TutorialPointController>().tutorialPointAction.Invoke(order);
    }
}
