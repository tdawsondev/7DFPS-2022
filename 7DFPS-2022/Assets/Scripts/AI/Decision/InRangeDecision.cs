using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/In Range Decision")]
public class InRangeDecision : Decision
{
    public float maxRange;
    public override bool Decide(BaseStateMachine stateMachine)
    {
        if(Vector3.Distance(stateMachine.transform.position, Player.Instance.transform.position) > maxRange)
            return false;
        return true;
    }
}
