using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Goal", menuName = "Quests/Goals/LevelGoal")]
public class LevelGoalSO : GoalSO
{
    public override Goal CreateGoal()
    {
        return new LevelGoal(this);
    }
}
