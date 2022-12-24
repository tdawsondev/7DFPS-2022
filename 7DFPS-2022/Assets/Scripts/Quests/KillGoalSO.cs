using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Goal", menuName = "Quests/Goals/KillGoal")]
public class KillGoalSO : GoalSO
{
    public EnemyType enemyType;
    public override Goal CreateGoal()
    {
        return new KillGoal(this);
    }
}
