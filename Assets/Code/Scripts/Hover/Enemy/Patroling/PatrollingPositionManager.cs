using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatrollingPositionManager : MonoBehaviour
{
    [SerializeField] Transform[] PatrollingPositions;
    [SerializeField] Color PatrollingColor = Color.white;
    [SerializeField] bool HidePath = false;

    private int currentTarget = 0;

    public Transform GetStartTransform()
    {
        currentTarget = Random.Range(0, PatrollingPositions.Length);
        return PatrollingPositions[currentTarget];
    }

    public Transform GetNextTarget()
    {
        Transform newTarget = PatrollingPositions[currentTarget];
        currentTarget++;

        if (currentTarget >= PatrollingPositions.Length)
            currentTarget = 0;

        return newTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Draw a line for the path
        if (PatrollingPositions.Length > 2 && !HidePath)
        {
            Handles.color = PatrollingColor;
            Vector3[] positions = new Vector3[PatrollingPositions.Length+1];
            for (int i = 0; i < PatrollingPositions.Length; i++)
            {
                positions[i] = PatrollingPositions[i].position;
            }
            positions[positions.Length-1] = positions[0];
            Handles.DrawPolyLine(positions);
        }
    }
#endif
}
