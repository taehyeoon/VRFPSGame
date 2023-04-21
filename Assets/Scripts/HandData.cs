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

    public HandModelType handType;          // 왼손, 오른손 구분
    public Transform root;                  // 손 Model
    public Animator animator;               // 손 Animator
    public Transform[] fingerBones;         // finger 관절 정보

}
