using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goal
{
    public Goal(GoalSO goal)
    {
        Description = goal.Description;
        RequiredAmount = goal.RequiredAmount;
        Completed = false;
        CurrentAmount = 0;
    }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public Quest Quest { get; set; }


    public virtual void Init()
    {
        // default init stuff
    }

    public void Evaluate()
    {
        HUDManager.instance.UpdateQuests();
        if (CurrentAmount >= RequiredAmount)
        {
            
            Complete();
        }
    }
    public virtual void Complete()
    {
        Completed = true;
        if(Quest != null)
        {
            Quest.CheckGoals();
        }
    }

}
