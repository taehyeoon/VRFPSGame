using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSocketController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        XRSocketInteractorTag xrSocketInteractorTag = GetComponent<XRSocketInteractorTag>();
        xrSocketInteractorTag.selectEntered.AddListener(AttachMagazine);
        xrSocketInteractorTag.selectExited.AddListener(DetachMagazine);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AttachMagazine(SelectEnterEventArgs args)
    {
        Transform interactable = args.interactableObject.transform;
        interactable.SetParent(gameObject.transform.root);

        MeshCollider collider = interactable.GetComponent<MeshCollider>();
        collider.enabled = false;
    }

    public void DetachMagazine(SelectExitEventArgs args)
    {
        Transform interactable = args.interactableObject.transform;
        interactable.SetParent(null);

        MeshCollider collider = args.interactableObject.transform.GetComponent<MeshCollider>();
        collider.enabled = true;

        Destroy(interactable.gameObject, 5.0f);
    }
}
