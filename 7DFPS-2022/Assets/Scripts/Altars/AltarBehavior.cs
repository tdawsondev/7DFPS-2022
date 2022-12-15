using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarBehavior : MonoBehaviour
{

    bool isActive = false;
    ParticleSystem particles;

    private void Start()
    {
        particles = gameObject.GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            if (other.tag == "Player")
            {
                Player.Instance.mana.Recharge();
                AudioManager.instance.StopSound("Recharge");
                AudioManager.instance.Play("Stinger");
                AudioManager.instance.Play("MainTheme");
                isActive = false;
                particles.Stop();
            }
        }

        if (other.tag == "Spell")
            Activate();
    }

    public void Activate()
    {
        isActive = true;
        particles.Play();
    }

}
