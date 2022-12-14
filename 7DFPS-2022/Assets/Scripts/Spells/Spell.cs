using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

    public SpellScriptableObject SpellToCast;
    public GameObject chargingPrefab;

    public abstract void CastSpell(Transform castPoint);

}
