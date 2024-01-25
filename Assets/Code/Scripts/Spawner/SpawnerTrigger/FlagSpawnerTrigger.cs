using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagSpawnerTrigger : MonoBehaviour
{
    [SerializeField] Spawner Spawner;
    public static UnityEvent<bool, int> FlagRemoved = new UnityEvent<bool, int>();

    private void Awake()
    {
        FlagRemoved.AddListener(SpawnFlag);
    }

    
    //bool -> player team
    private void SpawnFlag(bool team, int score)
    {
        if (team)
            Spawner.Spawn(0);
        else
            Spawner.Spawn(1);
    }
}
