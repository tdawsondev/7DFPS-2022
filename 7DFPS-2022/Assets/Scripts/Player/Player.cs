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
    }
    #endregion

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;
    public PlayerShooting shooting;
    public Health health;

    private void OnDestroy()
    {
        health.Damaged -= TookDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TookDamage(float amount)
    {
        HUDManager.instance.UpdateHealth();
    }
}
