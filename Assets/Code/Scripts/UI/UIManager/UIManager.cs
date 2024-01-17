using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] public ProgressBar SpeedVisualizer;
    [SerializeField] public TMP_Text Score;
    [SerializeField] public MiniMap MiniMap;
    [SerializeField] public RenderTexture RearViewMirror;
    [SerializeField] public DirectionVisualizer DirectionVisualizer;

    [Space]
    [Header("Player Reference")]
    [Tooltip("Player Reference")]
    [SerializeField] private PlayerManager PlayerManager;

    private void FixedUpdate()
    {
        SetUISpeed(PlayerManager.GetLinearVelocity());
        SetDirection();
    }

    public void PickUpFlag(int score)
    {
        Score.text += score;
    }

    public void SetUISpeed(float linearSpeed)
    {
        SpeedVisualizer.setProgress(linearSpeed);
    }

    public void SetDirection()
    {
        //DirectionVisualizer.VisualizeDirection();
    }
}
