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

    public static Action timeOut;

    private void Awake()
    {
        curLevelState = ELevelState.None;
        timeOut = () =>
        {
            Debug.Log("LevelController call timeOut");
            curLevelState = ELevelState.None;
            Destroy(curLevelSpawner);
            curLevelSpawner = null;
        };
    }

    public void EasyButtonClicked()
    {
        Destroy(curLevelSpawner);
        if (curLevelState == ELevelState.Easy)
        {
            Debug.Log("Level interruption");
            curLevelSpawner = null;
            curLevelState = ELevelState.None;
            TimeController.setPause(true);
        }
        else
        {
            Debug.Log("easy Level start");
            curLevelSpawner = Instantiate(easySpawner, easyPos.position, Quaternion.identity);
            curLevelState = ELevelState.Easy;
            TimeController.setPause(false);
        }
    }
}
