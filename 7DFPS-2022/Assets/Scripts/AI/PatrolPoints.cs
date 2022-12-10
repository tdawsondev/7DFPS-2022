using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolPoints : MonoBehaviour
{
    public List<Vector3> points;
    public float distanceError = 0.1f;
    private int currentIndex = 0;

    public Vector3 GetNextPoint()
    {
        currentIndex++;
        if(currentIndex >= points.Count)
        {
            currentIndex = 0;
            
        }
        return points[currentIndex];
    }

    public bool HasReachedPoint(NavMeshAgent navMeshAgent)
    {
        if(Vector3.Distance(points[currentIndex], navMeshAgent.transform.position) <= distanceError)
        {
            return true;
        }
        return false;
    }
}
