using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{
    [SerializeField] private ProjectileSpell projSpellToCast;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private float timeBetweenCasts = 0.25f;
    private float currentCastTimer;


    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;

    private void Update()
    {
        bool isSpellCastHeldDown = InputManager.Instance.IsCharging1();
        if (!castingMagic && isSpellCastHeldDown)
        {
            castingMagic = true;
            currentCastTimer = 0;
            //print("casting spell");
            CastSpell();
        }

        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCasts) castingMagic = false;
        }

        void CastSpell()
        {
            //cast spell
            print("proj cast");
            ProjectileSpell ps = Instantiate(projSpellToCast, castPoint.position, castPoint.rotation).GetComponent<ProjectileSpell>();
            ps.Launch(Camera.main.transform.forward);
            Debug.Log(castPoint.forward);
        }
    }
}
