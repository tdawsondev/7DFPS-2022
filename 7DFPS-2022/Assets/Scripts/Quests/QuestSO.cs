using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class QuestSO : ScriptableObject
{
    public List<GoalSO> Goals;
    public string QuestName;
    public string Description;
    public string WhichReward;
    public int Xpamount;
    public List<QuestSO> nextQuest;

    public Quest CreateQuest()
    {
        return new Quest(this);
    }
}
