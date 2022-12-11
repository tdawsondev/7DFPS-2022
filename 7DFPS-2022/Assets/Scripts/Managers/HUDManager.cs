using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of HUDmanager");
        }
        instance = this;
    }

    public Slider ManaSlider;
    public TextMeshProUGUI healthText; // temporary
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateManaSlider(int currentMana, int maxMana)
    {
        ManaSlider.maxValue = maxMana;
        ManaSlider.value = currentMana;
    }

    public void UpdateHealth()
    {
        healthText.text = "HP: " +Player.Instance.health.currentHP;
    }
}
