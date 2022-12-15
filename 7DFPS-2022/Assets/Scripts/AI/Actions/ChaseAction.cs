using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Chase")]
public class ChaseAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        NavMeshAgent navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
        LookAtTarget lookAtTarget = stateMachine.GetComponent<LookAtTarget>();
        if (lookAtTarget)
        {
            lookAtTarget.allowUpdate = false;
        }
        navMeshAgent.updateRotation = true;
        navMeshAgent.isStopped = false;

        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }
}
