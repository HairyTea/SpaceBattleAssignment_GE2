using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegoPath : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();

    public int current = 0;

    public bool isLooped = true;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 1; i < waypoints.Count; i++)
        {
            Gizmos.DrawLine(waypoints[i - 1], waypoints[i]);
            Gizmos.DrawSphere(waypoints[i - 1], 1);
            Gizmos.DrawSphere(waypoints[i], 1);

        }

        if (isLooped)
        {
            Gizmos.DrawLine(waypoints[waypoints.Count - 1], waypoints[0]);

        }
    }

    public Vector3 Next()
    {
        return waypoints[current];
    }

    public bool IsLast()
    {
        return (current == waypoints.Count - 1);
    }

    public void AdvanceToNext()
    {
        if (!isLooped)
        {
            if (!IsLast())
            {
                current++;
            }
        }
        else
        {
            current = (current + 1) % waypoints.Count;
        }
    }
}
