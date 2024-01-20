using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIFlagCounter : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image[] PlayerFlag = new Image[3];
    [SerializeField] Image[] EnemyFlag = new Image[3];
    [SerializeField] Sprite FlagEmpty;
    [SerializeField] Sprite FlagFull;

    public void SetScore(int playerScore, int enemyScore)
    {
        //empty color
        for (int i = 0; i < PlayerFlag.Length; i++)
        {
            PlayerFlag[i].sprite = FlagEmpty;
            EnemyFlag[i].sprite = FlagEmpty;
        }
        //update player sprite
        for (int i = 0; i < playerScore; i++)
        {
            PlayerFlag[i].sprite = FlagFull;
        }
        //update enemy sprite
        for (int i = 0; i < enemyScore; i++)
        {
            EnemyFlag[i].sprite = FlagFull;
        }
    }
}
