using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Patrolling Options")]
    [Tooltip("Position manager")]
    [SerializeField] PatrollingPositionManager PatrollingPositions;
    [Tooltip("If distance is less than this it will go towards the next position")]
    [SerializeField] float PatrollingSafeMargin;

}
