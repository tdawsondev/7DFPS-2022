using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBehavior : MonoBehaviour
{

    bool isActive = false;
    ParticleSystem particles;
    [SerializeField] Light activeLight;
    [SerializeField] float xpAmount;
    [SerializeField] GameObject altarText;

    private void Start()
    {
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particles.Stop();
        activeLight.gameObject.SetActive(false);
        altarText.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.tag == "Player")
            {
                Player.Instance.mana.Recharge();
                Player.Instance.manaLight.intensity = 2f;
                Player.Instance.magicSystem.cantCast = false;
                AudioManager.instance.StopSound("Recharge");
                AudioManager.instance.Play("Stinger");
                AudioManager.instance.Play("MainTheme");
                altarText?.SetActive(false);
                isActive = false;
                particles.Stop();
                ScoreManager.instance.GainXP(xpAmount);
                activeLight.gameObject.SetActive(false);
            }
        }
    }

    public void Activate()
    {
        isActive = true;
        particles.Play();
        activeLight.gameObject.SetActive(true);
        altarText?.SetActive(true);
    }

}
