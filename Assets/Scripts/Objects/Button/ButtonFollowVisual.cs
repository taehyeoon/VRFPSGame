using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5;
    public float followAngleThreshold;  // poke filter에 있는 threshold값과 동일하게 설정
    public float maxDistance;

    public Material originalMaterial;
    public Material pushedMaterial;

    private bool freeze = false;

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    private MeshRenderer visualTargetRenderer;  // material 바꾸기 위함

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;
        visualTargetRenderer = visualTarget.gameObject.GetComponent<MeshRenderer>();

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;

            // offset = 벡터의 방향 & 크기
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            // 찌르는 방향과 y축 -1방향 벡터와의 각도 계산 후, threshold값보다 작을때만 누를수있도록함
            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
            if(pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                freeze = false;
            }
        }
    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;

            visualTargetRenderer.material = originalMaterial;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 고정된 상태면 그냥 암것도 하지말기
        if (freeze)
            return;

        // XR Poke Interactor의 attachPoint 따라다니기
        if (isFollowing)
        {
            // 최대 거리까지 가면 그만가게하기
            if (Vector3.Distance(initialLocalPos, visualTarget.localPosition) > maxDistance)
            {
                freeze = true;
                isFollowing = false;

                visualTargetRenderer.material = pushedMaterial;
            }

            // 이동 축 제한
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }

        // 기존 자리로 돌아오기
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
