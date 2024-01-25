using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [Tooltip("Player height progress")]
    [SerializeField] private ProgressBar PlayerHeight_Progress;
    [Tooltip("Rapresent the stoplight timer")]
    [SerializeField] private ProgressBar StopLight_Progress;
    [Tooltip("Rapresent the shield timer")]
    [SerializeField] private ProgressBar Shield_Progress;
    [Space]
    [Header("Minimap")]
    [Tooltip("Minimap")]
    [SerializeField] public MiniMap MiniMap;
    [Header("Rear View")]
    [Tooltip("Rear view mirror")]
    [SerializeField] public RenderTexture RearViewMirror;
    [Space]
    [Header("Invisibility Effect")]
    [Tooltip("Invisibility Panel")]
    [SerializeField] public Image InvisibilityPanel;
    [Space]

    [Header("REFERENCES")]
    [Header("Game Manager Reference")]
    [Tooltip("Game manager reference used to see datas")]
    [SerializeField] private GameManager GameManagerInstance;
    [Header("Player Reference")]
    [Tooltip("Player Reference used to show parameters")]
    [SerializeField] private PlayerManager PlayerManager;

    private float ObscurationTimeLeft = 0f;
    private UsableManager UsableManager;
    private Movement PlayerMovement;

    private void Awake()
    {
        if (ObscureMap == null)
            ObscureMap = new UnityEvent<float>();

        UsableManager = PlayerManager.gameObject.GetComponent<UsableManager>();
        PlayerMovement = PlayerManager.gameObject.GetComponent<Movement>();

        ObscureMap.AddListener(OnObscureMap);
        MiniMap.Clear();

        InvisibilityPanel.enabled = false;

    }

    private void FixedUpdate()
    {
        //update all showed datas
        SetUISpeed(PlayerManager.GetLinearVelocity());
        SetUIScore(GameManager.Score);
        SetUIFlag(GameManager.PlayerFlags, GameManager.EnemyFlags);
        SetUIUsable(UsableManager.ObtainedUsableAmount[0], UsableManager.ObtainedUsableAmount[1], UsableManager.ObtainedUsableAmount[2]);
        SetDirection();

        //update timers
        UpdateTimer();
        
        //map obscuration
        if (ObscurationTimeLeft > 0)
        {
            ObscurationTimeLeft -= Time.fixedDeltaTime;
            if (ObscurationTimeLeft <= 0f || PlayerManager.Invulnerability)
                MiniMap.Clear();
        }

        //update height
        SetPlayerHeightVisualizer(GameManager.PlayerHeightClamped);

        //update invisibility effect
        if (EffectsManager.InvisibilityTime > 0)
            InvisibilityPanel.enabled = true;
        else
            InvisibilityPanel.enabled = false;
    }

    private void SetUISpeed(float linearSpeed)
    {
        SpeedVisualizer.SetProgress(linearSpeed);
    }

    private void SetUIScore(int score)
    {
        string fancyScore = score.ToString();
        int missingChar = 8-fancyScore.Length;
        fancyScore = "";
        for (int i = 0; i < missingChar; i++)
        {
            fancyScore += '0';
        }
        fancyScore += score;
        Score.SetText(fancyScore);
    }

    private void SetUIFlag(int playerF, int enemyF)
    {
        UIFlagCounter.SetScore(playerF, enemyF);
    }

    private void SetUIUsable(int p1, int p2, int p3)
    {
        UIUsables.SetUsableAmount(p1, p2, p3);
    }
    private void SetDirection()
    {
        DirectionVisualizer.VisualizeDirection();
    }
    
    private void UpdateTimer()
    {
        //usable
        UIUsables.SetProgress_Wall(EffectsManager.WallTime, EffectsManager.WallStartTime);
        UIUsables.SetProgress_Invisibility(EffectsManager.InvisibilityTime, EffectsManager.InvisibilityStartTime);
        //auto enabled collectible
        //Speed Edit
        //check if it can be updated
        if(EffectsManager.SpeedEditStartTime > 0)
        {
            //Set color based on malus
            if (PlayerMovement.SpeedLimiter > PlayerMovement.StartSpeedLimiter)
                StopLight_Progress.SetMalus(false);
            else
                StopLight_Progress.SetMalus(true);
            //update the progres bar
            float stopLightProgress = Mathf.Clamp01(EffectsManager.SpeedEditTime/EffectsManager.SpeedEditStartTime);
            StopLight_Progress.SetProgress(stopLightProgress);
        }
        //Invulnerability
        if(EffectsManager.InvulnerabilityStartTime > 0)
        {
            float shieldProgress = Mathf.Clamp01(EffectsManager.InvulnerabilityTime / EffectsManager.InvulnerabilityStartTime);
            Shield_Progress.SetProgress(shieldProgress);
        }
    }

    private void SetPlayerHeightVisualizer(float height)
    {
        PlayerHeight_Progress.SetProgress(height);
    }

    #region EVENTS CALLBACKs
    public void OnObscureMap(float obscureTime)
    {
        ObscurationTimeLeft = obscureTime;
        MiniMap.Obscure();
    }
    #endregion
}
