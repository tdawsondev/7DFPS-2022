using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{

    public float maxMana, currentMana;

    public delegate void LostMana(float amount);
    public event LostMana ManaLowered;

    public void LoseMana(float amount)
    {
        currentMana -= amount;
        if (currentMana > maxMana)
            currentMana = maxMana;
        if (currentMana <= 0)
        {
            currentMana = 0;
            AltarManager.instance.ActivateNewAltar();
            AudioManager.instance.StopSound("MainTheme");
            Player.Instance.magicSystem.cantCast = true;
            HUDManager.instance.DisplayInformation("Out of mana! Find the glowing altar to recharge");
            //AudioManager.instance.Play("Stinger");
            AudioManager.instance.Play("Recharge");
        }
        ManaLowered(amount);
    }

    public void Recharge()
    {
        LoseMana(-(maxMana - currentMana));
    }
    public void GainMana(float amount)
    {
        LoseMana(-amount);
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

    public void IncreaseMaxMana(float amount)
    {
        maxMana += amount;
        GainMana(amount);
    }

}
