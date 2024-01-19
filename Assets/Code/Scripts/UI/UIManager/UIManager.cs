using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    public static UnityEvent<float> ObscureMap = new UnityEvent<float>();

    [Header("UI Elements Speeds")]
    [Tooltip("Show the Player Acceleration")]
    [SerializeField] public ProgressBar SpeedVisualizer;
    [Space]

    [Header("Direction visulizer")]
    [SerializeField] public DirectionVisualizer DirectionVisualizer;
    [Space]
    [Header("Counters")]
    [Tooltip("Game score")]
    [SerializeField] private TMP_Text Score;
    [Tooltip("Flag counter")]
    [SerializeField] private UIFlagCounter UIFlagCounter;
    [Tooltip("Usable counter")]
    [SerializeField] private UIUsables UIUSables;
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

    private UsableManager PlayerUsable;

    private void Awake()
    {
        PlayerUsable = PlayerManager.GetComponent<UsableManager>();

        if (ObscureMap == null)
            ObscureMap = new UnityEvent<float>();

        ObscureMap.AddListener(OnObscureMap);
        MiniMap.Clear();
    }

    private void FixedUpdate()
    {
        SetUISpeed(PlayerManager.GetLinearVelocity());
        SetUIScore(GameManager.Score);
        SetUIFlag(GameManager.PlayerFlags, GameManager.EnemyFlags);
        SetUIUsable(PlayerUsable.ObtainedUsableAmount[0], PlayerUsable.ObtainedUsableAmount[1], PlayerUsable.ObtainedUsableAmount[2]);
    }

    private void SetUISpeed(float linearSpeed)
    {
        SpeedVisualizer.setProgress(linearSpeed);
    }

    private void SetUIScore(int score)
    {
        Score.SetText(score.ToString());
    }

    private void SetUIFlag(int playerF, int enemyF)
    {
        UIFlagCounter.SetScore(playerF, enemyF);
    }

    private void SetUIUsable(int p1, int p2, int p3)
    {
        UIUSables.SetUsableAmount(p1, p2, p3);
    }
    
    public void SetDirection()
    {
       
    }

    public void OnObscureMap(float obscureTime)
    {
        MiniMap.Obscure(obscureTime);
    }

    
}
