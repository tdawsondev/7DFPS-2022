using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{

    public float maxMana, currentMana;



    public void LoseMana(float amount)
    {
        currentMana -= amount;
    }

    public void Recharge()
    {
        currentMana = maxMana;
    }

    public bool OutOfMana
    {
        get
        {
            if (currentMana <= 0)
                return true;
            return false;
        }
    }

}
