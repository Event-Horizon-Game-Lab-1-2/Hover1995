using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public ProgressBar SpeedVisualizer;
    [SerializeField] public TMP_Text Score;
    [SerializeField] public MiniMap MiniMap;
    [SerializeField] public RenderTexture RearViewMirror;

    public void PickUpFlag(int score)
    {
        Score.text += score;
    }

    public void SetUISpeed(float linearSpeed)
    {
        SpeedVisualizer.setProgress(linearSpeed);
    }
}
