using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest 
{
    public QuestSO questSO;

    public Quest(QuestSO questSO)
    {
        this.questSO = questSO;
        Init();
    }

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }

    public void Init()
    {
        foreach(GoalSO goal in questSO.Goals)
        {
            Goals.Add(goal.CreateGoal());
        }
        QuestName = questSO.QuestName;
        Description = questSO.Description;
        Completed = false;
        Goals.ForEach(g => { g.Init(); g.Quest = this; });
        
    }

    //rewards

    public void CheckGoals()
    {
        if(Goals.All(g => g.Completed))
        {
            Complete();
        }

    }

    public void Complete()
    {
        Completed = true;
        GiveReward();
        
    }
    public void GiveReward()
    {

    }
}
