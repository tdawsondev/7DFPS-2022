using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
public class PatrolAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        NavMeshAgent navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        PatrolPoints patrolPoints = stateMachine.GetComponent<PatrolPoints>();
        if (!navMeshAgent.hasPath)
        {
            navMeshAgent.SetDestination(patrolPoints.GetNextPoint());
        }

        if (patrolPoints.HasReachedPoint(navMeshAgent))
            navMeshAgent.SetDestination(patrolPoints.GetNextPoint());
    }
}
