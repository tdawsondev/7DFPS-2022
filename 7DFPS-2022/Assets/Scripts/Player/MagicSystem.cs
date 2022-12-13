using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{

    [SerializeField] private Player player;

    public bool SystemLocked = false;
    [SerializeField] private ProjectileSpell projSpellToCastRH;
    [SerializeField] private ProjectileSpell projSpellToCastLH;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;

    private float currentCastTimerRH;
    private float currentCastTimerLH;
    private float currentChargeTimerRH;
    private float currentChargeTimerLH;

    [SerializeField] private Transform castPoint;

    private bool castingMagicRH = false;
    private bool castingMagicLH = false;
    private bool isChargingRH = false;
    private bool isChargingLH = false;

    private void Start()
    {
        HUDManager.instance.UpdateManaSlider();
    }

    private void Update()
    {

        RightMagic();
        LeftMagic();


        void RightMagic()
        {
            if (SystemLocked) return; // ignores update function if system is locked

            bool isSpellCastHeldDown = InputManager.Instance.IsCharging1();
            bool isSpellReleasing = InputManager.Instance.IsUnReleasing1();

            if (isSpellCastHeldDown && !castingMagicRH)
            {
                isChargingRH = true;
                currentChargeTimerRH += Time.deltaTime;
                HUDManager.instance.UpdateChargeRH(currentChargeTimerRH);
            }

            if (isSpellReleasing && isChargingRH)
            {
                castingMagicRH = true;
                isChargingRH = false;

                if (currentChargeTimerRH >= projSpellToCastRH.SpellToCast.ChargeTime)
                    CastSpell(projSpellToCastRH);

                currentCastTimerRH = 0;
                currentChargeTimerRH = 0;
                HUDManager.instance.UpdateChargeRH(currentChargeTimerRH);
            }

            //gives a small cooldown between casting spells
            if (castingMagicRH)
            {
                currentCastTimerRH += Time.deltaTime;

                if (currentCastTimerRH > projSpellToCastRH.SpellToCast.TimeBetweenCasts)
                    castingMagicRH = false;
            }
        }

        void LeftMagic()
        {
            if (SystemLocked) return; // ignores update function if system is locked

            bool isSpellCastHeldDown = InputManager.Instance.IsCharging2();
            bool isSpellReleasing = InputManager.Instance.IsUnReleasing2();

            if (isSpellCastHeldDown && !castingMagicLH)
            {
                isChargingLH = true;
                currentChargeTimerLH += Time.deltaTime;
                HUDManager.instance.UpdateChargeLH(currentChargeTimerLH);
            }

            if (isSpellReleasing && isChargingLH)
            {
                castingMagicLH = true;
                isChargingLH = false;

                if (currentChargeTimerLH >= projSpellToCastLH.SpellToCast.ChargeTime)
                    CastSpell(projSpellToCastLH);

                currentCastTimerLH = 0;
                currentChargeTimerLH = 0;
                HUDManager.instance.UpdateChargeLH(currentChargeTimerLH);
            }

            //gives a small cooldown between casting spells
            if (castingMagicLH)
            {
                currentCastTimerLH += Time.deltaTime;

                if (currentCastTimerLH > projSpellToCastLH.SpellToCast.TimeBetweenCasts)
                    castingMagicLH = false;
            }
        }

        void CastSpell(ProjectileSpell projectileSpell)
        {
            if (Player.Instance.mana.currentMana >= projectileSpell.SpellToCast.ManaCost)
            {
                ProjectileSpell ps = Instantiate(projectileSpell, castPoint.position, castPoint.rotation).GetComponent<ProjectileSpell>();
                ps.Launch(Camera.main.transform.forward, "Enemy");

                //Lower mana
                Player.Instance.mana.LoseMana(projectileSpell.SpellToCast.ManaCost);
                HUDManager.instance.UpdateManaSlider();
            }
        }


    }

}
