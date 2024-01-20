using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUsables : MonoBehaviour
{
    [Header("Power Up Quantity")]
    [SerializeField] TMP_Text PowerUp1;
    [SerializeField] TMP_Text PowerUp2;
    [SerializeField] TMP_Text PowerUp3;

    [SerializeField] ProgressBar PowerUp1_Progress;
    [SerializeField] ProgressBar PowerUp2_Progress;
    [SerializeField] ProgressBar PowerUp3_Progress;

    public void SetUsableAmount(int p1, int p2, int p3)
    {
        PowerUp1.SetText(""+p1);
        PowerUp1.SetText(""+p2);
        PowerUp1.SetText(""+p3);
    }

    public void SetHeightProgress(float value, float startValue)
    {
        PowerUp1_Progress.SetProgress(value/startValue);
    }

    public void SetProgress_Wall(float value, float startValue)
    {
        PowerUp2_Progress.SetProgress(value/startValue);
    }

    public void SetProgress_Invisibility(float value, float startValue)
    {
        PowerUp3_Progress.SetProgress(value/startValue);
    }
}
