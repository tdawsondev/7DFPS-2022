using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Melee")]
public class MeleeAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        AIMelee melee = stateMachine.GetComponent<AIMelee>();

        if (melee.readyForMelee)
        {
            melee.Swing();
        }

    }
}
