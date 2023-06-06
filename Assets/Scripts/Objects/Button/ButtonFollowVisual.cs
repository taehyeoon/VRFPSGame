using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5;
    public float followAngleThreshold;  // poke filter�� �ִ� threshold���� �����ϰ� ����
    public float maxDistance;

    public Material originalMaterial;
    public Material pushedMaterial;

    private bool freeze = false;

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttachTransform;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    private MeshRenderer visualTargetRenderer;  // material �ٲٱ� ����

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

            // offset = ������ ���� & ũ��
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            // ��� ����� y�� -1���� ���Ϳ��� ���� ��� ��, threshold������ �������� �������ֵ�����
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
        // ������ ���¸� �׳� �ϰ͵� ��������
        if (freeze)
            return;

        // XR Poke Interactor�� attachPoint ����ٴϱ�
        if (isFollowing)
        {
            // �ִ� �Ÿ����� ���� �׸������ϱ�
            if (Vector3.Distance(initialLocalPos, visualTarget.localPosition) > maxDistance)
            {
                freeze = true;
                isFollowing = false;

                visualTargetRenderer.material = pushedMaterial;
            }

            // �̵� �� ����
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        }

        // ���� �ڸ��� ���ƿ���
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
