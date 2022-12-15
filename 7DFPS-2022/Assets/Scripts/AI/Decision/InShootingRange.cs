using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/In Shooting Range Decision")]
public class InShootingRange : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        EnemySightSensor sightSensor = stateMachine.GetComponent<EnemySightSensor>();
        AIShooting aIShooting = stateMachine.GetComponent<AIShooting>();
        float distance = Vector3.Distance(stateMachine.transform.position, Player.Instance.transform.position);

        if (sightSensor.CanSeePlayer() && distance <= aIShooting.shootingRange)
            return true;
        return false;
    }
}
