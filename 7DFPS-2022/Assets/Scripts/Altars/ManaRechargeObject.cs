using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRechargeObject : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.Instance.mana.Recharge();
            Destroy(this.gameObject);
        }
    }


}
