using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Quest 
{
    public QuestSO questSO;
    public List<QuestSO> nextQuest;

    public Quest(QuestSO questSO)
    {
        this.questSO = questSO;
        Init();
    }

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }

    public string whichReward;// this is all sloppy too, but also for prototyping. Can be reworked with actual inventory system.
    public int xpAmount;

    public void Init()
    {
        foreach(GoalSO goal in questSO.Goals)
        {
            Goals.Add(goal.CreateGoal());
        }
        QuestName = questSO.QuestName;
        Description = questSO.Description;
        xpAmount = questSO.Xpamount;
        whichReward = questSO.WhichReward;
        nextQuest = questSO.nextQuest;
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
        QuestManager.instance.RemoveQuest(this);
        if(nextQuest != null)
        {
            foreach (QuestSO quest in nextQuest)
            {
                QuestManager.instance.AddQuest(quest.CreateQuest());
            }
        }
        GiveReward();
        
    }
    public void GiveReward()
    {
        if(whichReward != "") // replace this with actual xp system
        {
            if(whichReward == "Blue")
            {
                Player.Instance.hasBlue = true;
            }
            if (whichReward == "Red")
            {
                Player.Instance.hasRed = true;
            }
            if (whichReward == "Green")
            {
                Player.Instance.hasGreen = true;
            }
            HUDManager.instance.UpdateQuestItems();
        }
        if(xpAmount > 0)
        {
           ScoreManager.instance.GainXP(xpAmount);
        }
    }
}
