using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetActiveGrabHand : MonoBehaviour
{
    public HandData rightHandPose;
    public HandData leftHandPose;

    public Renderer rightRenderer;
    public Renderer leftRenderer;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnSetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);
    }

    // Hand Pose Set up
    public void SetupPose(BaseInteractionEventArgs arg)
    {
        // 손이 닿으면 GameObject SetActive
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();

            // 오른손
            if (handData.handType == HandData.HandModelType.Right)
            {
                rightHandPose.gameObject.SetActive(true);
                rightRenderer.enabled = false;
            }

            // 왼손
            else
            {
                leftHandPose.gameObject.SetActive(true);
                leftRenderer.enabled = false;
            }
        }
    }

    // SetUpPose와 반대
    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();

            // 오른손
            if (handData.handType == HandData.HandModelType.Right)
            {
                rightHandPose.gameObject.SetActive(false);
                rightRenderer.enabled = true;
            }

            // 왼손
            else
            {
                leftHandPose.gameObject.SetActive(false);
                leftRenderer.enabled = true;
            }
        }
    }
}
