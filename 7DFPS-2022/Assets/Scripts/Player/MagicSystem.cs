using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSystem : MonoBehaviour
{

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

    public Animator rightHandAnim;
    public Animator rightHandCharge;
    public Animator leftHandAnim;
    public Animator lefttHandCharge;

    private AudioSource chargingSound;

    private void Start()
    {
        HUDManager.instance.UpdateManaSlider();
        rightHandAnim.SetFloat("ChargeSpeed", 1.1f+spellToCastRH.SpellToCast.ChargeTime / 0.792f);
        HUDManager.instance.UpdateChargeLH(currentChargeTimerLH, spellToCastLH.SpellToCast.ChargeTime);
        HUDManager.instance.UpdateChargeRH(currentChargeTimerRH, spellToCastRH.SpellToCast.ChargeTime);

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
                rightHandAnim.SetBool("Charging", true);
                rightHandCharge.SetBool("Charging", true);
                isChargingRH = true;
                currentChargeTimerRH += Time.deltaTime;
                HUDManager.instance.UpdateChargeRH(currentChargeTimerRH, spellToCastRH.SpellToCast.ChargeTime);
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
                        rightHandAnim.SetTrigger("Casting");
                        rightHandCharge.SetBool("Charging", false);

                        spellToCastRH.CastSpell(castPoint);
                        AudioManager.instance.Play("SpellCast");
                    }
                }
                rightHandAnim.SetBool("Charging", false);
                rightHandCharge.SetBool("Charging", false);



                currentCastTimerRH = 0;
                currentChargeTimerRH = 0;
                HUDManager.instance.UpdateChargeRH(currentChargeTimerRH, spellToCastRH.SpellToCast.ChargeTime);
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
                leftHandAnim.SetBool("Charging", true);
                lefttHandCharge.SetBool("Charging", true);
                isChargingLH = true;
                currentChargeTimerLH += Time.deltaTime;
                HUDManager.instance.UpdateChargeLH(currentChargeTimerLH, spellToCastLH.SpellToCast.ChargeTime);
            }

            if (isSpellReleasing && isChargingLH)
            {
                if (chargingSound != null)
                {
                    chargingSound.Stop();
                }
                castingMagicLH = true;
                isChargingLH = false;

                if (currentChargeTimerLH >= spellToCastLH.SpellToCast.ChargeTime)
                {
                    if (HasEnoughMana(spellToCastLH))
                    {
                        leftHandAnim.SetTrigger("Casting");
                        lefttHandCharge.SetBool("Charging", false);
                        spellToCastLH.CastSpell(castPoint);
                        AudioManager.instance.Play("SpellCast");
                    }

                }
                leftHandAnim.SetBool("Charging", false);
                lefttHandCharge.SetBool("Charging", false);

                currentCastTimerLH = 0;
                currentChargeTimerLH = 0;
                HUDManager.instance.UpdateChargeLH(currentChargeTimerLH, spellToCastLH.SpellToCast.ChargeTime);
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
