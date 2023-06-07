using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    private Pistol _pistol;

    private void Awake()
    {
        _pistol = transform.parent.parent.GetComponent<Pistol>();
    }

    private void OnEnable()
    {
        _pistol.RefreshAmmoUI();
    }
}
