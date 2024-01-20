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
    [SerializeField] private UIUsables UIUsables;
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

    private float ObscurationTimeLeft = 0f;
    private UsableManager UsableManager;

    private void Awake()
    {
        if (ObscureMap == null)
            ObscureMap = new UnityEvent<float>();

        UsableManager = PlayerManager.gameObject.GetComponent<UsableManager>();

        ObscureMap.AddListener(OnObscureMap);
        MiniMap.Clear();
    }

    private void FixedUpdate()
    {
        //update all showed datas
        SetUISpeed(PlayerManager.GetLinearVelocity());
        SetUIScore(GameManager.Score);
        SetUIFlag(GameManager.PlayerFlags, GameManager.EnemyFlags);
        SetUIUsable(UsableManager.ObtainedUsableAmount[0], UsableManager.ObtainedUsableAmount[1], UsableManager.ObtainedUsableAmount[2]);

        //update timers
        UpdateTimers();
        
        //map obscuration
        if (ObscurationTimeLeft > 0)
        {
            ObscurationTimeLeft -= Time.fixedDeltaTime;
            if (ObscurationTimeLeft <= 0f)
                MiniMap.Clear();
        }
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
        UIUsables.SetUsableAmount(p1, p2, p3);
    }
    
    public void SetDirection()
    {
       
    }

    public void OnObscureMap(float obscureTime)
    {
        ObscurationTimeLeft = obscureTime;
        MiniMap.Obscure();
    }

    public void UpdateTimers()
    {
        UIUsables.SetProgress_Wall(EffectsManager.WallTime, EffectsManager.WallStartTime);
        UIUsables.SetProgress_Invisibility(EffectsManager.InvisibilityTime, EffectsManager.InvisibilityStartTime);
    }
}
