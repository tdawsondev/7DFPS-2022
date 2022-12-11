using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Move")]
public class MoveAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        NavMeshAgent navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        MoveToPoint moveTo = stateMachine.GetComponent<MoveToPoint>();
        if (!moveTo.hasPoint)
        {
            moveTo.SetRandomPointOnMesh();
        }
        navMeshAgent.SetDestination(moveTo.targetPoint);
    }
}
