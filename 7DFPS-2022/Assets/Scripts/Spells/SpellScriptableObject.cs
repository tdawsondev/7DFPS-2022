using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells")]
public class SpellScriptableObject : ScriptableObject
{

    //Spell constants
    public float ManaCost = 5f;
    public float TimeBetweenCasts = 0.5f;
    public float ChargeTime = 1.0f;

    //Projectile spell variables
    public float Damage = 10f;
    public float LifeTime = 2f;
    public float Speed = 15f;
    public float SpellRadius = 0.5f;

    //Buff spell variables
    public float Healing = 3f;

    //Status effects

}
