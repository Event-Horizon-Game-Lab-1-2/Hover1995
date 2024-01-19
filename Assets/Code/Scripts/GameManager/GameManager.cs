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

    [SerializeField] UIManager UI;
    [SerializeField] PlayerManager Player;

    [SerializeField] private int FlagToWin = 3;
    [SerializeField] private int PointForEachMissingEnemyFlag = 2500;

    [HideInInspector] public int EnemyFlags { get; private set; } = 0;
    [HideInInspector] public int PlayerFlags { get; private set; } = 0;

    public int Score = 0;

    private void Start()
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
            PlayerFlags--;
            Score -= flagScore;
            PlayerFlags = Mathf.Clamp(PlayerFlags, 0, FlagToWin);
            if(Score < 0)
                Score = 0;
        }
        else
        {
            EnemyFlags--;
            EnemyFlags = Mathf.Clamp(EnemyFlags, 0, FlagToWin);
        }
    }

    private void EndGame()
    {
        Score += PointForEachMissingEnemyFlag * (FlagToWin - EnemyFlags);

        if (PlayerFlags >= FlagToWin)
        {
            Debug.Log("PLAYER WON");

        }
        else
        {
            Debug.Log("ENEMY WON");
        }
    }

}
