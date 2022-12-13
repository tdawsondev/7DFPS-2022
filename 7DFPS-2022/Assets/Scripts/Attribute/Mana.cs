using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{

    public float maxMana, currentMana;

    // event that allows you to check whenever mana is changed
    public delegate void LostMana(float amount);
    public event LostMana ManaLowered;

    public void LoseMana(float amount)
    {
        currentMana -= amount;
        ManaLowered(amount);
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
