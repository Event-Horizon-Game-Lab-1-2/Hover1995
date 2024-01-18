using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIFlagCounter : MonoBehaviour
{
    [SerializeField] Image[] PlayerFlag = new Image[3];
    [SerializeField] Image[] EnemyFlag = new Image[3];


    [SerializeField] Color EmptyColor = Color.white;
    [SerializeField] Color FillColorPlayer = Color.blue;
    [SerializeField] Color FillColorEnemy = Color.red;

    //[SerializeField] Image FlagsEmpty;
    //[SerializeField] Image FlagsFull;

    public void SetScore(int playerScore, int enemyScore)
    {
        //empty color
        for (int i = 0; i < PlayerFlag.Length; i++)
        {
            PlayerFlag[i].color = EmptyColor;
            EnemyFlag[i].color = EmptyColor;
        }

        for (int i = 0; i < playerScore; i++)
        {
            PlayerFlag[i].color = FillColorPlayer;
        }

        for (int i = 0; i < enemyScore; i++)
        {
            EnemyFlag[i].color = FillColorEnemy;
        }
    }
}
