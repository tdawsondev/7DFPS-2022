using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Shoot")]
public class ShootAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        NavMeshAgent agent = stateMachine.GetComponent<NavMeshAgent>();
        if (agent.hasPath)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
        agent.updateRotation = false;
        LookAtTarget lookAtTarget = stateMachine.GetComponent<LookAtTarget>();
        if (lookAtTarget)
        {
            lookAtTarget.allowUpdate = true;
        }

        AIShooting shooting = stateMachine.GetComponent<AIShooting>();

        if (shooting.readyToShoot)
        {
            shooting.StartShooting(2);
        }

    }
}
