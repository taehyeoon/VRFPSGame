using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public enum HandModelType
    {
        Left,
        Right
    }

    public HandModelType handType;          // �޼�, ������ ����
    public Transform root;                  // �� Model
    public Animator animator;               // �� Animator
    public Transform[] fingerBones;         // finger ���� ����

}
