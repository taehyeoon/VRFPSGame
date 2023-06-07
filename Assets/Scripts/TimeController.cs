using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float easyLimitTime;
    public float remainingTime;
    
    public TMP_Text timeText;
    public Image backGroundImage;
    private LevelController lc;
    private bool isPause;

    public static Action<bool> setPause;

    private void Awake()
    {
        isPause = false;
        lc = GameObject.Find("LevelController").GetComponent<LevelController>();
        RefreshTimeUI();
        
        setPause = (val) =>
        {
            // stop
            if (val)
            {
                isPause = true;
            }
            // start
            else
            {
                isPause = false;
                remainingTime = easyLimitTime;
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
