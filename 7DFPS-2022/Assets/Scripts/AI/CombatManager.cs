using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    private void Awake()
    {
        instance = this;
    }

    public delegate void OnEnemyDeath(BaseEnemy enemy);

    public event OnEnemyDeath EnemyDied;

    public void EnemyKilled(BaseEnemy enemy)
    {
        if(EnemyDied != null)
            EnemyDied(enemy);
    }
}
