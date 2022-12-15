using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalEnemy : BaseEnemy
{
    public float minFlyHeight;
    public float maxFlyHeight;

    public override void OnSpawn()
    {
        base.OnSpawn();
        agent.height = Random.Range(minFlyHeight, maxFlyHeight);
        agent.baseOffset = agent.height - 0.5f;
        
    }
}
