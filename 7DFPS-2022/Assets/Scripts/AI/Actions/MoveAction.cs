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
        LookAtTarget look = stateMachine.GetComponent<LookAtTarget>();
        if (look)
        {
            look.allowUpdate = false;
        }
        if (!moveTo.hasPoint)
        {
            moveTo.SetRandomPointNearPlayer();
        }
        navMeshAgent.updateRotation = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(moveTo.targetPoint);
    }
}
