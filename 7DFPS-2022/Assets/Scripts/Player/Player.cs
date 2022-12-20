using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region SingletonAwake
    public static Player Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of player");
        }
        Instance = this;
        health.Damaged += TookDamage;
        mana.ManaLowered += LostMana;
    }
    #endregion

    public PlayerMovement playerMovement;
    public MagicSystem magicSystem;
    public MouseLook mouseLook;
    public Health health;
    public Mana mana;
    public Light manaLight;
    public bool IgnoreDeath = false;
    public Animator hitAnimator;
    public ParticleSystem healAnim;

    private void OnDestroy()
    {
        health.Damaged -= TookDamage;
        mana.ManaLowered -= LostMana;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LockPlayer()
    {
        playerMovement.movementDisabled = true;
        mouseLook.lookLocked = true;
        magicSystem.SystemLocked = true;
    }
    public void UnlockPlayer()
    {
        playerMovement.movementDisabled = false;
        mouseLook.lookLocked = false;
        magicSystem.SystemLocked = false;

    }

    public void TookDamage(float amount, Transform tran)
    {
        
        HUDManager.instance.UpdateHealth();

        if (amount > 0)
        {
            AudioManager.instance.Play("PlayerHit");
            //Hit Direction
            Vector3 direction = transform.position - tran.position;
            float frontDot = Vector3.Dot(direction, transform.forward);
            float rightDot = Vector3.Dot(direction, transform.right);

            if (Mathf.Abs(frontDot) > Mathf.Abs(rightDot))
            {
                if (frontDot > 0)
                {
                    HUDManager.instance.StartDecay(HUDManager.instance.Bottom);
                    hitAnimator.Play("PlayerHitLeft");
                }
                else
                {
                    HUDManager.instance.StartDecay(HUDManager.instance.Top);
                    hitAnimator.Play("PlayerHitRight");

                }
            }
            else
            {
                if (rightDot > 0)
                {
                    HUDManager.instance.StartDecay(HUDManager.instance.Left);
                    hitAnimator.Play("PlayerHitLeft");

                }
                else
                {
                    HUDManager.instance.StartDecay(HUDManager.instance.Right);
                    hitAnimator.Play("PlayerHitRight");

                }
            }

            if (health.Dead && !IgnoreDeath)
            {
                MenuController.instance.OpenGameOverMenu();
            }
        }
    }

    public void LostMana(float amount)
    {
        HUDManager.instance.UpdateManaSlider();
        if (mana.OutOfMana)
        {
            manaLight.intensity = 1f;
        }
    }
}
