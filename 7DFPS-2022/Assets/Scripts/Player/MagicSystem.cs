using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{

    public bool SystemLocked = false;
    [SerializeField] private ProjectileSpell projSpellToCast;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private float timeBetweenCasts = 0.5f;
    private float currentCastTimer;
    private float currentChargeTimer;


    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;
    private bool isCharging = false;

    private void Update()
    {
        if (SystemLocked) return; // ignores update function if system is locked
        bool isSpellCastHeldDown = InputManager.Instance.IsCharging1();
        bool isSpellReleasing = InputManager.Instance.IsUnReleasing1();

        if (isSpellCastHeldDown && !castingMagic)
        {
            isCharging = true;
            currentChargeTimer += Time.deltaTime;
            HUDManager.instance.UpdateCharge(currentChargeTimer);
        }

        if (isSpellReleasing && isCharging)
        {
            castingMagic = true;
            isCharging = false;
            currentCastTimer = 0;
            currentChargeTimer = 0;
            CastSpell();
        }

        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCasts)
                castingMagic = false;
        }


        void CastSpell()
        {
            //cast spell
            ProjectileSpell ps = Instantiate(projSpellToCast, castPoint.position, castPoint.rotation).GetComponent<ProjectileSpell>();
            ps.Launch(Camera.main.transform.forward, "Enemy");
        }
    }
}
