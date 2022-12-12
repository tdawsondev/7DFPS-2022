using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public Health health;
    private void Awake()
    {
        health.Damaged += TookDamage;
    }
    private void OnDestroy()
    {
        health.Damaged -= TookDamage;
    }

    public void TookDamage(float amount)
    {
        if (health.Dead)
        {
            Destroy(gameObject);
        }

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
