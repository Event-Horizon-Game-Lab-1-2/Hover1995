using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagSpawnerTrigger : MonoBehaviour
{
    [SerializeField] Spawner Spawner;

    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.FlagRemoved.AddListener(SpawnFlag);
        else
            Debug.LogWarning("Unable To get Game Manager Listener");
    }

    //bool -> player team
    private void SpawnFlag(bool team, int score)
    {
        if (team)
        {
            if(GameManager.PlayerFlags+1>=0)
                Spawner.Spawn(0);
        }
        else
        {
            if (GameManager.EnemyFlags+1>=0)
                Spawner.Spawn(1);
        }
    }
}
