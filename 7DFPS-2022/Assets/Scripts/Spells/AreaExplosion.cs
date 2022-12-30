using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExplosion : MonoBehaviour
{
    public float damageRange = 5f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        BaseEnemy[] enemies = FindObjectsOfType<BaseEnemy>();
        foreach (BaseEnemy enemy in enemies)
        {
            if (Vector3.Distance(enemy.transform.position, transform.position) <= damageRange)
            {
                enemy.GetComponent<Health>().Damage(damage);
            }
        }
    }

    // Update is called once per frame

    
}
