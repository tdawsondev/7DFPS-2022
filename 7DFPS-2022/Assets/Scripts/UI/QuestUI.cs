using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{

    public RectTransform rectTransform;
    public Quest quest;
    public TMP_Text nameText;
    public GameObject TitleTextPrefab;
    public GameObject GoalUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }
    public void UpdateQuest(Quest q) // this can be optimized later.
    {
        quest = q;
        float height = quest.Goals.Count * 25f + 25f;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        rectTransform.sizeDelta= new Vector2(rectTransform.sizeDelta.x, height);
        nameText = Instantiate(TitleTextPrefab, transform).GetComponent<TMP_Text>();
        nameText.text = quest.QuestName;
        foreach(Goal goal in quest.Goals)
        {
            GoalUI goalUI = Instantiate(GoalUIPrefab, transform).GetComponent<GoalUI>();
            goalUI.UpdateGoal(goal);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
