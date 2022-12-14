using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{

    [SerializeField] private Player player;

    public bool SystemLocked = false;
    [SerializeField] private Spell spellToCastRH;
    [SerializeField] private Spell spellToCastLH;

    private float currentCastTimerRH;
    private float currentCastTimerLH;
    private float currentChargeTimerRH;
    private float currentChargeTimerLH;

    [SerializeField] private Transform castPoint;

    private bool castingMagicRH = false;
    private bool castingMagicLH = false;
    private bool isChargingRH = false;
    private bool isChargingLH = false;

    private AudioSource chargingSound;

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
                if (chargingSound == null || !chargingSound.isPlaying && HasEnoughMana(spellToCastRH))
                {
                    chargingSound = AudioManager.instance.Play("SpellCharge");
                }
                isChargingRH = true;
                currentChargeTimerRH += Time.deltaTime;
                HUDManager.instance.UpdateChargeRH(currentChargeTimerRH);
            }

            if (isSpellReleasing && isChargingRH)
            {
                if(chargingSound != null)
                {
                    chargingSound.Stop();
                }
                castingMagicRH = true;
                isChargingRH = false;

                if (currentChargeTimerRH >= spellToCastRH.SpellToCast.ChargeTime)
                {
                    if (HasEnoughMana(spellToCastRH))
                    {
                        spellToCastRH.CastSpell(castPoint);
                        AudioManager.instance.Play("SpellCast");
                    }
                }

                currentCastTimerRH = 0;
                currentChargeTimerRH = 0;
                HUDManager.instance.UpdateChargeRH(currentChargeTimerRH);
            }

            //gives a small cooldown between casting spells
            if (castingMagicRH)
            {
                currentCastTimerRH += Time.deltaTime;

                if (currentCastTimerRH > spellToCastRH.SpellToCast.TimeBetweenCasts)
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
                if(chargingSound == null || !chargingSound.isPlaying && HasEnoughMana(spellToCastLH))
                {
                    chargingSound = AudioManager.instance.Play("SpellCharge");
                }
                isChargingLH = true;
                currentChargeTimerLH += Time.deltaTime;
                HUDManager.instance.UpdateChargeLH(currentChargeTimerLH);
            }

            if (isSpellReleasing && isChargingLH)
            {
                castingMagicLH = true;
                isChargingLH = false;

                if (currentChargeTimerLH >= spellToCastLH.SpellToCast.ChargeTime)
                {
                    if (HasEnoughMana(spellToCastLH))
                    {
                        spellToCastLH.CastSpell(castPoint);
                        AudioManager.instance.Play("SpellCast");
                    }

                }

                currentCastTimerLH = 0;
                currentChargeTimerLH = 0;
                HUDManager.instance.UpdateChargeLH(currentChargeTimerLH);
            }

            //gives a small cooldown between casting spells
            if (castingMagicLH)
            {
                currentCastTimerLH += Time.deltaTime;

                if (currentCastTimerLH > spellToCastLH.SpellToCast.TimeBetweenCasts)
                    castingMagicLH = false;
            }
        }

        //void CastSpell(Spell projectileSpell)
        //{
        //    if (Player.Instance.mana.currentMana >= projectileSpell.SpellToCast.ManaCost)
        //    {
        //        ProjectileSpell ps = Instantiate(projectileSpell, castPoint.position, castPoint.rotation).GetComponent<ProjectileSpell>();
        //        ps.Launch(Camera.main.transform.forward, "Enemy");

        //        //Lower mana
        //        Player.Instance.mana.LoseMana(projectileSpell.SpellToCast.ManaCost);
        //    }
        //}


    }

    public bool HasEnoughMana(Spell spell)
    {
        if(Player.Instance.mana.currentMana >= spell.SpellToCast.ManaCost)
        {
            return true;
        }
        return false;
    }

}
