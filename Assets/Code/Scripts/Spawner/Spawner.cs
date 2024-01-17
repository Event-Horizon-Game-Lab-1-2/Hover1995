using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Positions")]
    [Tooltip("List of the position")]
    [SerializeField] PositionController PositionController;
    [Space]
    [Header("Spawn Settings")]
    [Tooltip("Array of the object to spawn")]
    [SerializeField] public SpawnerData[] ObjectsToGenerate;
    [Tooltip("Does the interactables respawn when interacted?")]
    [SerializeField] public bool RespawnOnTaken = true;
    
    //Interactable referces
    private List<Interactable> InteractableList;
    private List<Transform> InteractablePositionList;



    private void Awake()
    {
        InteractableList = new List<Interactable>();
        InteractablePositionList = new List<Transform>();
    }

    private void Start()
    {
        //spawn all objects
        for (int i = 0; i < ObjectsToGenerate.Length; i++)
        {
            for (int j = 0; j < ObjectsToGenerate[i].MaxSpawnAmount; j++)
            {
                Interactable newObject = ObjectsToGenerate[i].InteractableToSpawn;
                Transform newposition = PositionController.GetRandomPosition();

                InteractableList.Add(newObject);
                InteractablePositionList.Add(newposition);

                Instantiate<Interactable>(newObject, newposition.position, Quaternion.identity);
            }
        }
    }
    public bool CanSpawn()
    {
        if (!RespawnOnTaken)
            return false;

        bool canSpawn = true;
        //in a cycle to avoid removed object in the same frame error
        for(int i = 0; i < ObjectsToGenerate.Length;i++)
        {
            if (InteractableList[i] == null)
            {
                PositionController.RecoverPosition(InteractablePositionList[i]);
                InteractablePositionList.RemoveAt(i);
                canSpawn = false;
            }
        }

        return canSpawn;
    }

    public void Spawn()
    {
        Debug.Log("Spawn");
        int randomObject = UnityEngine.Random.Range(0, ObjectsToGenerate.Length - 1);

        Interactable newObject = ObjectsToGenerate[randomObject].InteractableToSpawn;
        Transform newposition = PositionController.GetRandomPosition();

        InteractableList.Add(newObject);
        InteractablePositionList.Add(newposition);

        Instantiate<Interactable>(newObject, newposition.position, Quaternion.identity);
    }

}
