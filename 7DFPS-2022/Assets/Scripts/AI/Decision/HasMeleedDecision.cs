using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Has Meleed")]
public class HasMeleedDecision : Decision
{
    public int swings;
    public override bool Decide(BaseStateMachine stateMachine)
    {
        AIMelee melee = stateMachine.GetComponent<AIMelee>();
        if(melee.currentNumberofSwings >= swings)
        {
            melee.ResetCurrentSwings();
            return true;
        }
        return false;
    }
}
