using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image CurrentImage;

    [SerializeField] private Color MalusColor = Color.red;
    [SerializeField] private Color BonusColor = Color.yellow;


    private void Awake()
    {
        CurrentImage.color = BonusColor;
    }

    public void SetProgress(float progress)
    {
        CurrentImage.fillAmount = progress;
    }

    public void SetMalus(bool malus)
    {
        CurrentImage.color = malus ? MalusColor : BonusColor;
    }
}
