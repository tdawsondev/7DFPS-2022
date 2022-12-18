using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBehavior : MonoBehaviour
{

    bool isActive = false;
    ParticleSystem particles;
    [SerializeField] Light activeLight;
    [SerializeField] Outline outline;
    [SerializeField] float xpAmount;

    private void Start()
    {
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particles.Stop();
        activeLight.gameObject.SetActive(false);
        outline.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.tag == "Player")
            {
                Player.Instance.mana.Recharge();
                Player.Instance.manaLight.intensity = 2f;
                AudioManager.instance.StopSound("Recharge");
                AudioManager.instance.Play("Stinger");
                AudioManager.instance.Play("MainTheme");
                isActive = false;
                particles.Stop();
                outline.enabled = false;
                ScoreManager.instance.GainXP(xpAmount);
                activeLight.gameObject.SetActive(false);
            }
        }

        if (other.tag == "Spell")
            Activate();
    }

    public void Activate()
    {
        isActive = true;
        particles.Play();
        activeLight.gameObject.SetActive(true);
        outline.enabled = true;
    }

}
