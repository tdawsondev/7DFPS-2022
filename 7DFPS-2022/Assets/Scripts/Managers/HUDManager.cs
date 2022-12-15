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
    public Slider HealthSlider;
    public TextMeshProUGUI chargeRHText; // temporary
    public TextMeshProUGUI chargeLHText; //temporary

    public CanvasGroup Top, Bottom, Left, Right;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateManaSlider()
    {
        ManaSlider.maxValue = Player.Instance.mana.maxMana;
        ManaSlider.value = Player.Instance.mana.currentMana;
    }

    public void UpdateHealth()
    {
        HealthSlider.maxValue = Player.Instance.health.maxHP;
        HealthSlider.value = Player.Instance.health.currentHP;
    }

    public void UpdateChargeRH(float currentCharge)
    {
        chargeRHText.text = "Charge Right: " + currentCharge.ToString("n2");
    }

    public void UpdateChargeLH(float currentCharge)
    {
        chargeLHText.text = "Charge Left: " + currentCharge.ToString("n2");
    }

    public void StartDecay(CanvasGroup group)
    {
        group.alpha = 1f;
        StartCoroutine(Decay(group));
    }
    IEnumerator Decay(CanvasGroup group)
    {
        while(group.alpha > 0f)
        {
            group.alpha -= 1f *Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
