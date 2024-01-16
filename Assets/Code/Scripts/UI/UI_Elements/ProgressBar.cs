using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image image;

    public void setProgress(float progress)
    {
        image.fillAmount = progress;
    }
}
