using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSO : ScriptableObject
{
    public string Description;
    public int RequiredAmount;

    public virtual Goal CreateGoal()
    {
        return new Goal(this);
    }
}
