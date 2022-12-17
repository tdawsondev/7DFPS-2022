using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Actions/Melee")]
public class MeleeAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        AIMelee melee = stateMachine.GetComponent<AIMelee>();
        NavMeshAgent agent = stateMachine.GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        LookAtTarget lookAtTarget = stateMachine.GetComponent<LookAtTarget>();
        if (lookAtTarget)
        {
            lookAtTarget.allowUpdate = true;
        }

        if (melee.readyForMelee)
        {
            melee.Swing();
        }

    }
}
