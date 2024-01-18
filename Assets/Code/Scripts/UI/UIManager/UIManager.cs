using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements Speeds")]
    [Tooltip("Show the Player Acceleration")]
    [SerializeField] public ProgressBar SpeedVisualizer;
    [Space]

    [Header("Direction visulizer")]
    [SerializeField] public DirectionVisualizer DirectionVisualizer;
    [Space]
    [Header("Scores")]
    [Tooltip("Game score")]
    [SerializeField] public TMP_Text Score;
    [Tooltip("Game score")]
    [SerializeField] private UIFlagCounter UIFlagCounter;
    [Space]
    [Header("Minimap")]
    [Tooltip("Minimap")]
    [SerializeField] public MiniMap MiniMap;
    [Header("Rear View")]
    [Tooltip("Rear view mirror")]
    [SerializeField] public RenderTexture RearViewMirror;
    [Space]

    [Header("Game Manager Reference")]
    [Tooltip("Game manager reference used to see datas")]
    [SerializeField] private GameManager GameManager;
    [Header("Player Reference")]
    [Tooltip("Player Reference used to show parameters")]
    [SerializeField] private PlayerManager PlayerManager;

    private void FixedUpdate()
    {
        SetUISpeed(PlayerManager.GetLinearVelocity());
        SetUIScore(GameManager.Score);
        SetUIFlag(GameManager.PlayerFlags, GameManager.EnemyFlags);
    }

    public void SetUISpeed(float linearSpeed)
    {
        SpeedVisualizer.setProgress(linearSpeed);
    }

    public void SetUIScore(int score)
    {
        Score.SetText(score.ToString());
    }

    public void SetUIFlag(int playerF, int enemyF)
    {
        UIFlagCounter.SetScore(playerF, enemyF);
    }

    public void SetDirection()
    {
       
    }
}
