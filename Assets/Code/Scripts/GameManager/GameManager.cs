using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    //bool -> player team
    //int -> Score
    public static UnityEvent<bool, int> FlagTaken;
    public static UnityEvent<bool, int> FlagRemoved;

    public static GameManager Instance { get; private set; }

    [SerializeField] PlayerManager Player;

    [SerializeField] private int FlagToWin = 3;
    [SerializeField] private int PointForEachMissingEnemyFlag = 2500;
    [Space]
    [SerializeField] public float MaxHeight = 20f;

    [HideInInspector] public static int EnemyFlags { get; private set; } = 0;
    [HideInInspector] public static int PlayerFlags { get; private set; } = 0;
    
    [HideInInspector] public static float PlayerHeightClamped;

    [SerializeField] private String VictoryText = "WON";
    [SerializeField] private String DefeatText = "LOST";

    public static int Score = 0;
    private Transform PlayerTransform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        DontDestroyOnLoad(gameObject);

        if (FlagTaken == null)
            FlagTaken = new UnityEvent<bool, int>();
        if (FlagRemoved == null)
            FlagRemoved = new UnityEvent<bool, int>();

        FlagTaken.AddListener(OnFlagTaken);
        FlagRemoved.AddListener(OnFlagRemoved);

        PlayerTransform = Player.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        PlayerHeightClamped = PlayerTransform.position.y;
        if(PlayerHeightClamped > MaxHeight)
        {
            PlayerTransform.position = new Vector3(PlayerTransform.position.x , MaxHeight, PlayerTransform.position.z);
        }
        PlayerHeightClamped /= MaxHeight;
        Mathf.Clamp01(PlayerHeightClamped);
    }

    private void OnFlagTaken(bool playerTeam, int flagScore)
    {
        if(playerTeam)
        {
            PlayerFlags++;
            Score += flagScore;
            if (PlayerFlags >= FlagToWin)
                EndGame();
        }
        else
        {
            EnemyFlags++;
            if (EnemyFlags >= FlagToWin)
                EndGame();
        }
    }

    private void OnFlagRemoved(bool playerTeam, int flagScore)
    {
        if (playerTeam)
        {
            if(PlayerFlags > 0)
            {
                PlayerFlags--;
                Score -= flagScore;
                FlagSpawnerTrigger.FlagRemoved.Invoke(playerTeam, flagScore);
            }
            PlayerFlags = Mathf.Clamp(PlayerFlags, 0, FlagToWin);
            if(Score < 0)
                Score = 0;
        }
        else
        {
            if (EnemyFlags > 0)
            {
                EnemyFlags--;
                FlagSpawnerTrigger.FlagRemoved.Invoke(playerTeam, flagScore);
            }
            EnemyFlags = Mathf.Clamp(EnemyFlags, 0, FlagToWin);
        }
    }

    private void EndGame()
    {
        Score += PointForEachMissingEnemyFlag * (FlagToWin - EnemyFlags);

        if (PlayerFlags >= FlagToWin)
        {
            UIManager.EndGame.Invoke(VictoryText);
            Player.GetComponent<Movement>().Halt();
        }
        else
        {
            UIManager.EndGame.Invoke(DefeatText);
            Player.GetComponent<Movement>().Halt();
        }
    }

}
