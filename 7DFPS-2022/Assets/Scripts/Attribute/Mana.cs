using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{

    public float maxMana, currentMana;

    public delegate void LostMana(float amount);
    public event LostMana ManaLowered;

    public AltarManager altarManager;

    public void LoseMana(float amount)
    {
        currentMana -= amount;
        if (currentMana > maxMana)
            currentMana = maxMana;
        if (currentMana <= 0)
        {
            currentMana = 0;
            altarManager.ActivateNewAltar();
        }
        ManaLowered(amount);
    }

    public void Recharge()
    {
        LoseMana(-(maxMana - currentMana));
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
