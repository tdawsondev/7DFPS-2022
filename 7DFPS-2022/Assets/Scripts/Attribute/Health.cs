using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHP, currentHP;

    // event that alows you to check whenever damage is taken
    public delegate void TookDamage(float amount);
    public event TookDamage Damaged;

    public void Damage(float amount)
    {
        currentHP -= amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        if (currentHP < 0)
            currentHP = 0;
        Damaged(amount);
    }

    public void Kill()
    {
        Damage(currentHP);
    }

    public void Heal(float amount)
    {
        Damage(-amount);
    }

    public bool Dead
    {
        get
        {
            if (currentHP <= 0)
                return true;
            return false;
        }
    }
}
