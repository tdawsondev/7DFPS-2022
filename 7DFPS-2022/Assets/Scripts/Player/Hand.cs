using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public string leftOrRight;
    public Spell spell;

    private float currentChargeTimer;
    private bool chargeReady;
    private Transform castPoint;

    private bool castingMagic = false;
    public bool isCharging = false; // gets set in magic system

    public Animator handAnim;
    public Animator chargeAnim;

    private AudioSource chargingSound;

    public bool cantCast = false;

    private void Start()
    {
        castingMagic = false;
        currentChargeTimer = 0f;
        castPoint = Player.Instance.magicSystem.castPoint;
        handAnim.SetFloat("ChargeSpeed", 1.1f + spell.SpellToCast.ChargeTime / 0.792f);
        UpdateHUD();

    }

    public void OnRelease()
    {
        if (chargeReady && HasEnoughMana(spell))
        {
            //cast spell
            handAnim.SetTrigger("Casting");
            chargeAnim.SetBool("Charging", false);
            AudioManager.instance.Play("SpellCast");
            castingMagic = true;
            spell.CastSpell(castPoint);
            StartCoroutine(WaitForSpellToCast());
            

        }
        currentChargeTimer = 0f;
        chargeReady = false;
        UpdateHUD();
    }
    IEnumerator WaitForSpellToCast()
    {
        yield return new WaitForSeconds(spell.SpellToCast.TimeBetweenCasts);
        castingMagic = false;
    }

    private void Update()
    {
        if (isCharging && !castingMagic)
        {
            if (HasEnoughMana(spell)) {

                if (chargingSound == null || !chargingSound.isPlaying) { 
                    chargingSound = AudioManager.instance.Play("SpellCharge"+leftOrRight); //hacky fix for other hand cutting off sound
                }
                handAnim.SetBool("Charging", true);
                chargeAnim.SetBool("Charging", true);

                currentChargeTimer += Time.deltaTime;
                if (currentChargeTimer >= spell.SpellToCast.ChargeTime)
                {
                    currentChargeTimer = spell.SpellToCast.ChargeTime;
                    chargeReady = true;
                }
                UpdateHUD();
            }
            else
            {
                StopCharging();
            }
        }
        else
        {
            StopCharging();
        }
    }

    private void UpdateHUD()
    {
        if(leftOrRight == "Right")
        {
            HUDManager.instance.UpdateChargeRH(currentChargeTimer, spell.SpellToCast.ChargeTime);
        }
        else
        {
            HUDManager.instance.UpdateChargeLH(currentChargeTimer, spell.SpellToCast.ChargeTime);
        }
    }

    private void StopCharging()
    {
        StopChargingSound();
        handAnim.SetBool("Charging", false);
        chargeAnim.SetBool("Charging", false);
        currentChargeTimer = 0f;
        chargeReady = false;
    }

    private void StopChargingSound()
    {
        if (chargingSound && chargingSound.isPlaying)
        {
            chargingSound.Stop();
        }
    }

    public bool HasEnoughMana(Spell spell)
    {
        if (Player.Instance.mana.currentMana >= spell.SpellToCast.ManaCost)
        {
            return true;
        }
        return false;
    }

}
