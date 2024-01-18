using System;
using UnityEngine;

[System.Serializable]
public class SpawnerData : MonoBehaviour
{
    [Tooltip("Object to spawn")]
    public Interactable InteractableToSpawn;
    [Tooltip("Max Amount of spawnable object of this type")]
    public int MaxSpawnAmount;
    [Tooltip("Spawn probability")]
    [HideInInspector][Range(0.0f, 1.0f)] public float SpawnProbability = 1.0f;
}
