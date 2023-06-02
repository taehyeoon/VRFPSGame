using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetActiveGrabHand : MonoBehaviour
{
    public HandData rightHandPose;
    public HandData leftHandPose;

    public Renderer rightRenderer;
    public Renderer leftRenderer;

    public HandData.HandModelType CurrentHand { get; private set; } // unsetpose �� None, setupPose �� Left or Right


    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(SetupPose);
        grabInteractable.selectExited.AddListener(UnSetPose);

        rightHandPose.gameObject.SetActive(false);
        leftHandPose.gameObject.SetActive(false);

        CurrentHand = HandData.HandModelType.None;
    }

    // Hand Pose Set up
    public void SetupPose(BaseInteractionEventArgs arg)
    {
        // ���� ������ GameObject SetActive
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();

            // ������
            if (handData.handType == HandData.HandModelType.Right)
            {
                CurrentHand = HandData.HandModelType.Right;

                rightHandPose.gameObject.SetActive(true);
                rightRenderer.enabled = false;
            }

            // �޼�
            else
            {
                CurrentHand = HandData.HandModelType.Left;

                leftHandPose.gameObject.SetActive(true);
                leftRenderer.enabled = false;
            }
        }
    }

    // SetUpPose�� �ݴ�
    public void UnSetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();

            CurrentHand = HandData.HandModelType.None;

            // ������
            if (handData.handType == HandData.HandModelType.Right)
            {
                rightHandPose.gameObject.SetActive(false);
                rightRenderer.enabled = true;
            }

            // �޼�
            else
            {
                leftHandPose.gameObject.SetActive(false);
                leftRenderer.enabled = true;
            }
        }
    }
}
