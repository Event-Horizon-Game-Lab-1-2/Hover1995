using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Spawner[] Spawners;
    [SerializeField][Range(0.1f, 10f)] private float RespawnTime = 1f;

    public static UnityEvent InteractableTriggred;

    void Start()
    {
        if (InteractableTriggred == null)
            InteractableTriggred = new UnityEvent();

        InteractableTriggred.AddListener(AllocateInteractable);
    }

    private void AllocateInteractable()
    {
        StartCoroutine(AddObject());
    }

    IEnumerator AddObject()
    {
        yield return new WaitForSeconds(RespawnTime);
        for (int i = 0; i < Spawners.Length; i++)
        {
            if (Spawners[i].CanSpawn())
                Spawners[i].Spawn();
        }
    }
}
