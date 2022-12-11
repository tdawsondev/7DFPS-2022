using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Arrived")]
public class ArrivedDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        MoveToPoint moveTo = stateMachine.GetComponent<MoveToPoint>();

        if (moveTo.ArrivedAtPoint())
        {
            moveTo.hasPoint = false;
            return true;
        }
        return false;
    }
}
