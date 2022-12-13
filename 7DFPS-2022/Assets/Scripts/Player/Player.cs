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

    public void TookDamage(float amount)
    {
        HUDManager.instance.UpdateHealth();
    }

    public void LostMana(float amount)
    {
        HUDManager.instance.UpdateManaSlider();
    }
}
