using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    public Health health;
    public NavMeshAgent agent;
    private void Awake()
    {
        health.Damaged += TookDamage;
    }
    private void OnDestroy()
    {
        health.Damaged -= TookDamage;
    }

    public virtual void TookDamage(float amount, Transform tran)
    {
        if (health.Dead)
        {
            SpawnManager.instance.spawnCount--;
            Destroy(gameObject);
        }

    }

    /// <summary>
    /// Used to Set variables when an enemy is spawned in
    /// </summary>
    public virtual void OnSpawn()
    {

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
