using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private void Awake()
    {
        instance = this;
    }

    public List<Quest> quests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        HUDManager.instance.UpdateQuests();
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
        HUDManager.instance.UpdateQuests();
    }

}
