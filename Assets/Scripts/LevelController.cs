using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELevelState
{
    None,
    Easy,
    Normal,
    Hard,
}
public class LevelController : MonoBehaviour
{
    public GameObject easySpawner;
    public Transform easyPos;
    public ELevelState curLevelState;
    public GameObject curLevelSpawner;
    
    
    private void Awake()
    {
        curLevelState = ELevelState.None;
    }

    public void EasyButtonClicked()
    {
        Destroy(curLevelSpawner);
        if (curLevelState == ELevelState.Easy)
        {
            Debug.Log("Level interruption");
            curLevelSpawner = null;
            curLevelState = ELevelState.None;
        }
        else
        {
            Debug.Log("easy Level start");
            curLevelSpawner = Instantiate(easySpawner, easyPos.position, Quaternion.identity);
            curLevelState = ELevelState.Easy;
        }
    }
}
