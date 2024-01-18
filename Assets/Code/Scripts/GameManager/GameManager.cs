using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public static UnityEvent<bool, int> FlagTaken = new UnityEvent<bool, int>();

    public static GameManager Instance { get; private set; }

    [SerializeField] UIManager UI;
    [SerializeField] PlayerManager Player;

    [SerializeField] private int FlagToWin = 3;

    [HideInInspector] public int EnemyFlags;
    [HideInInspector] public int PlayerFlags;

    public int Score = 0;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        
        DontDestroyOnLoad(gameObject);

        if (FlagTaken == null)
            FlagTaken = new UnityEvent<bool, int>();

        FlagTaken.AddListener(OnFlagTaken);
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
    private void EndGame()
    {
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
