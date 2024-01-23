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

    private Transform GetNextTarget()
    {
        currentTarget++;
        return PatrollingPositions[currentTarget - 1];
    }

    public Transform GetCloserWanderingPos(Transform currentPos)
    {
        Transform closerPoint = currentPos;
        for (int i = 0; i < PatrollingPositions.Length; i++)
        {
            if (Vector3.Distance(PatrollingPositions[i].position, closerPoint.position) < 0f)
            {

            }
        }
        return closerPoint;
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
