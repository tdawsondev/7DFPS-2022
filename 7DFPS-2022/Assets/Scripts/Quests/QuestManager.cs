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
    public QuestSO starterQuest;

    public void Start()
    {
        AddQuest(starterQuest.CreateQuest(), false);
    }

    public void AddQuest(Quest quest, bool notify = true)
    {
        quests.Add(quest);
        HUDManager.instance.UpdateQuests();
        if (notify)
        {
            AudioManager.instance.Play("UIPositive");
            HUDManager.instance.DisplayInformation("New Challenge Added!");
        }
    }

    public void RemoveQuest(Quest quest)
    {
        quests.Remove(quest);
        HUDManager.instance.UpdateQuests();
    }

}
