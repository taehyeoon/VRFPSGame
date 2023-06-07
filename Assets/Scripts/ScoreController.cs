using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static Action kill;
    public TMP_Text killText;
    private int killScore;
    // Use to prevent a function from running as soon as it starts
    private bool shouldExecute;

    private void Awake()
    {
        killScore = 0;
        killText.text = killScore.ToString();
        kill = () =>
        {
            if (shouldExecute)
            {
                Kill();
            }
        };

        shouldExecute = true;
    }

    private void Kill()
    {
        Debug.Log("Kill call");
        killScore += 1;
        killText.text = killScore.ToString();
    }
}
