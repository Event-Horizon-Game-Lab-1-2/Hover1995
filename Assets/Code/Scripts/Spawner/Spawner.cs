using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
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
    private List<Transform> InteractableTransforms;

    private void Awake()
    {
        InteractableList = new List<Interactable>();
        InteractableTransforms = new List<Transform>();
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

                InteractableTransforms.Add(newposition);
                InteractableList.Add( Instantiate<Interactable>(newObject, newposition.position, Quaternion.identity) );
            }
        }
    }

    public void Spawn()
    {
        if (!RespawnOnTaken)
            return;

        //Update old spawn object position
        updateInteractableList();

        //select what object spawn
        int randomObject = UnityEngine.Random.Range(0, ObjectsToGenerate.Length - 1);
        Interactable newObject = ObjectsToGenerate[randomObject].InteractableToSpawn;
        //choose a random position

        Transform newposition = PositionController.GetRandomPosition();

        InteractableTransforms.Add(newposition);
        InteractableList.Add(Instantiate<Interactable>(newObject, newposition.position, Quaternion.identity));
    }
    
    public void recoverSpawnPoint(Transform transform)
    {
        //free that position
        PositionController.RecoverPosition(transform);
    }

    private void updateInteractableList()
    {
        for (int i = 0; i < ObjectsToGenerate.Length; i++)
        {
            //find a removed object
            if (InteractableList[i] == null)
            {
                InteractableList.RemoveAt(i);
                recoverSpawnPoint(InteractableTransforms[i].transform);
                InteractableTransforms.RemoveAt(i);

            }
        }
    }

    private void debugPrintList()
    {
        for(int i = 0; i< InteractableList.Count; i++)
        {
            Debug.Log(InteractableList[i].ToString());
        }
    }
}
