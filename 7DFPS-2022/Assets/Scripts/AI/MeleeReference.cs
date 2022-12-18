using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeReference : MonoBehaviour
{
    [SerializeField] AIMelee melee;

    public void CheckDamage()
    {
        melee.CheckDamage();
    }
    public void RechargeMelee()
    {
        melee.StartMeleeRecharge();
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
