using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGoal : Goal
{
    public LevelGoal(GoalSO goalSO) : base(goalSO)
    {
        Description = goalSO.Description;
        Completed = false;
        CurrentAmount = 0;
        RequiredAmount = goalSO.RequiredAmount;
    }

    public override void Init()
    {
        base.Init();
        ScoreManager.instance.LeveledUp += LeveledUp;
    }

    public void LeveledUp(int newLevel)
    {
        CurrentAmount = newLevel;
        Evaluate();
    }

    public override void Complete()
    {
        base.Complete();
        ScoreManager.instance.LeveledUp -= LeveledUp;

    }
}
