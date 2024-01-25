using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableSpawnerTrigger : MonoBehaviour
{
    [SerializeField] private Spawner SpawnerWithRespawn;
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
        StartCoroutine(SpawnNewObject());
    }

    IEnumerator SpawnNewObject()
    {
        yield return new WaitForSeconds(RespawnTime);
        SpawnerWithRespawn.Spawn();
    }
}