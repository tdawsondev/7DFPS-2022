using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPoint : MonoBehaviour
{
    public float maxRandomDistance;
    [HideInInspector]
    public bool hasPoint = false;
    
    [HideInInspector]
    public Vector3 targetPoint;

    public float errorDistance = 2.5f;

    private void Update()
    {
        
    }

    public void SetRandomPointNearPlayer()
    {
        hasPoint = true;
        Vector3 randomDirection = Random.insideUnitSphere * maxRandomDistance;
        randomDirection+= Player.Instance.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxRandomDistance, 1);
        targetPoint = hit.position;
        
    }

    public bool ArrivedAtPoint()
    {
        if (Vector3.Distance(transform.position, targetPoint) < errorDistance)
        {
            return true;
        }
        return false;
    }
}
