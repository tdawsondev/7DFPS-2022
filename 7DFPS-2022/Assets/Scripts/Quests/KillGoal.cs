using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillGoal : Goal
{
    public EnemyType EnemyType { get; set; }

    public KillGoal(KillGoalSO goalSO) : base(goalSO)
    {
        EnemyType = goalSO.enemyType;
        Description = goalSO.Description;
        Completed = false;
        CurrentAmount = 0;
        RequiredAmount = goalSO.RequiredAmount;
    }

    public override void Init()
    {
        base.Init();
        CombatManager.instance.EnemyDied += EnemyDied;
    }

    void EnemyDied(BaseEnemy enemy)
    {
        if(EnemyType == EnemyType.Any || enemy.EnemyType == EnemyType)
        {
            CurrentAmount++;
            Evaluate();
        }
    }
    public override void Complete()
    {
        base.Complete();
        CombatManager.instance.EnemyDied -= EnemyDied;
    }
}
