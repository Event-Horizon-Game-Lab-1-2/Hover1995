using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PositionController: MonoBehaviour
{

    [Header("Gizmo Settings")]
    [Tooltip("Size of the visible gizmo, the object will be generated at the center of the sphere")]
    [SerializeField][Range(0.1f, 2f)] float SphereSize = 0.2f;
    [SerializeField] Color SphereColor = Color.magenta;

    [Header("Spawner Settings")]
    [Tooltip("Array of the positions upon which the object will be generated, the position is chosen randomly")]
    [SerializeField] List<Transform> SpawnsPositions;

    private List<Transform> FreeSpawns;
    private List<Transform> UsedSpawns;

    private void Awake()
    {
        UsedSpawns = new List<Transform>();
        FreeSpawns = SpawnsPositions;
    }

    public void RecoverPosition(Transform positionToRecover)
    {
        UsedSpawns.Remove(positionToRecover);
        FreeSpawns.Add(positionToRecover);
    }

    public void OccupyPosition(Transform positionToOccupy)
    {
        FreeSpawns.Remove(positionToOccupy);
        UsedSpawns.Add(positionToOccupy);
    }

    public Transform GetRandomPosition()
    {
        int index = UnityEngine.Random.Range(0, FreeSpawns.Count-1);
        Transform positionChosen = FreeSpawns[index];

        OccupyPosition(positionChosen);

        return positionChosen;
    }

    private void OnDrawGizmos()
    {
    #if UNITY_EDITOR
        for (int i = 0; i < SpawnsPositions.Count; i++)
        {
            Gizmos.color = SphereColor;
            Gizmos.DrawSphere(SpawnsPositions[i].position, SphereSize);
        }
    #endif
        if(Application.isPlaying) 
        {
            for (int i = 0; i < FreeSpawns.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(FreeSpawns[i].position, SphereSize);
            }

            for (int i = 0; i < UsedSpawns.Count; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(UsedSpawns[i].position, SphereSize);
            }
        }
    }
}