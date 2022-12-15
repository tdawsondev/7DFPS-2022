using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Has Shot")]
public class HasShotDecision : Decision
{
    public float bursts;
    public override bool Decide(BaseStateMachine stateMachine)
    {
        AIShooting shooting = stateMachine.GetComponent<AIShooting>();
        
        if (shooting.doneShooting)
        {
            return true;
        }
        return false;
    }
}
