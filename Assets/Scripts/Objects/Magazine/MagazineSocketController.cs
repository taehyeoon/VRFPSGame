using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSocketController : MonoBehaviour
{
    public GameObject magazineShowed;

    // Start is called before the first frame update
    void Start()
    {
        XRSocketInteractorTag xrSocketInteractorTag = GetComponent<XRSocketInteractorTag>();
        xrSocketInteractorTag.selectEntered.AddListener(AttachMagazine);
        xrSocketInteractorTag.selectExited.AddListener(DetachMagazine);

    }

    public void AttachMagazine(SelectEnterEventArgs args)
    {
        Transform interactable = args.interactableObject.transform;
        interactable.SetParent(gameObject.transform.root);
        interactable.GetComponent<Renderer>().enabled = false;
        if(interactable.childCount > 1)
        {
            for(int i = 0; i < interactable.childCount; i++)
            {
                interactable.GetChild(i).gameObject.SetActive(false);
            }
        }

        magazineShowed.SetActive(true);
    }

    public void DetachMagazine(SelectExitEventArgs args)
    {
        Transform interactable = args.interactableObject.transform;
        interactable.SetParent(null);
        interactable.GetComponent<Renderer>().enabled = true;
        if (interactable.childCount > 1)
        {
            for (int i = 0; i < interactable.childCount; i++)
            {
                interactable.GetChild(i).gameObject.SetActive(true);
            }
        }

        magazineShowed.SetActive(false);

        Destroy(interactable.gameObject, 5.0f);
    }
}
