using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosManager : MonoBehaviour
{
    [Header("Position Settings")]
    [SerializeField] Transform[] SpawnsPositions;
    [Header("Player")]
    [SerializeField] GameObject Player;

    [Header("Gizmo Settings")]
    [Tooltip("Size of the visible gizmo, the object will be generated at the center of the sphere")]
    [SerializeField][Range(0.1f, 2f)] float SphereSize = 0.2f;
    [SerializeField] Color SphereColor = Color.magenta;

    private void Awake()
    {
        int index = Random.Range(0, SpawnsPositions.Length);
        Player.transform.position = SpawnsPositions[index].position;
        Player.transform.rotation = SpawnsPositions[index].rotation;
    }

    private void OnDrawGizmos()
    {
    #if UNITY_EDITOR
        for (int i = 0; i < SpawnsPositions.Length; i++)
        {
            Gizmos.color = SphereColor;
            Gizmos.DrawSphere(SpawnsPositions[i].position, SphereSize);
        }
    #endif
    }
}
