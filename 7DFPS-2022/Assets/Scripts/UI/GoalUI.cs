using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalUI : MonoBehaviour
{
    public TMP_Text text;
    public Goal goal;
    public void UpdateGoal(Goal goal)
    {
        text.text = "  " + goal.Description +": " + goal.CurrentAmount + "/" + goal.RequiredAmount;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
