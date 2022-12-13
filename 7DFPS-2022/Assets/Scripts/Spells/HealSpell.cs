using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpell : Spell
{

    public override void CastSpell(Transform castPoint)
    {
        if (Player.Instance.mana.currentMana >= SpellToCast.ManaCost)
        {
            Player.Instance.health.Heal(SpellToCast.Healing);
        }
    }

}
