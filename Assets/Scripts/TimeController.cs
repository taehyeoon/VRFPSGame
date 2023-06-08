using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float easyLimitTime;
    public float normalLimitTime;
    public float hardLimitTime;
    public float remainingTime;
    
    public TMP_Text timeText;
    public Image backGroundImage;
    private LevelController lc;
    private bool isPause;

    public static Action<bool, ELevelState> setPause;

    private void Awake()
    {
        isPause = true;
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        RefreshTimeUI();
        
        setPause = (isPauseInput, levelState) =>
        {
            // stop
            if (isPauseInput)
            {
                isPause = true;
            }
            // start
            else
            {
                isPause = false;
                if(levelState == ELevelState.Easy)
                    remainingTime = easyLimitTime;
                else if (levelState == ELevelState.Normal)
                    remainingTime = normalLimitTime;
                else if (levelState == ELevelState.Hard)
                    remainingTime = hardLimitTime;
            }
        };
    }
    
    private void Update()
    {
        if(isPause) return;
        
        if (remainingTime <= 0)
        {
            isPause = true;
            remainingTime = 0;
            RefreshTimeUI();
            LevelController.timeOut();
            return;
        }
        
        if (remainingTime <= 10f)
        {
            float t = Mathf.InverseLerp(0f, 10f, remainingTime);
        }
        remainingTime -= Time.deltaTime;
        RefreshTimeUI();

    }

    private void RefreshTimeUI()
    {
        int min = (int)Math.Truncate(remainingTime / 60f);
        int sec = (int)(remainingTime - (min * 60f));
        timeText.text = min.ToString("D2")+":"+sec.ToString("D2");
    }
    
}
