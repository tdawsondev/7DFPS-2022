using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpQuest : MonoBehaviour
{

    public QuestSO quest;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            QuestManager.instance.AddQuest(quest.CreateQuest());
            Destroy(gameObject);
        }
    }
}
