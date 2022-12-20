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
    public Slider RHSlider; 
    public Slider LHSlider; 
    public TextMeshProUGUI xpText; //temporary
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI informationText;
    public CanvasGroup informationTextCanvas;
    public GameObject OutOfManaText;
    public GameObject LowManaText;
    public CanvasGroup lowHealthBorder;
    public TMP_Text healthText;
    public TMP_Text manaText;

    public CanvasGroup Top, Bottom, Left, Right;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth();
        DisplayInformation("Slay enemies and stay alive as long as you can...");
    }

    public void UpdateManaSlider()
    {
        float currentMana = Player.Instance.mana.currentMana;
        float maxMana = Player.Instance.mana.maxMana;
        float percentMana = currentMana / maxMana;
        manaText.text = currentMana.ToString() + "/" + maxMana.ToString();
        ManaSlider.maxValue = Player.Instance.mana.maxMana;
        ManaSlider.value = Player.Instance.mana.currentMana;
        LowManaText.SetActive(false);
        OutOfManaText.SetActive(false);
        if(percentMana < .25f)
        {
            LowManaText.SetActive(true);
        }
        if(currentMana <= 0)
        {
            LowManaText.SetActive(false);
            OutOfManaText.SetActive(true);
        }

    }

    public void UpdateHealth()
    {
        float currentHP = Player.Instance.health.currentHP;
        float maxHP = Player.Instance.health.maxHP;
        HealthSlider.maxValue = maxHP;
        HealthSlider.value = currentHP;
        healthText.text = currentHP.ToString() +"/"+ maxHP.ToString(); 

        float percentHealth = currentHP / maxHP;
        if(percentHealth < .45f) // if below 45%
        {
            lowHealthBorder.alpha = 1-( percentHealth * 2.222f);
            if(percentHealth < .1f)
            {
                lowHealthBorder.alpha = 1f;
            }
        }
        else
        {
            lowHealthBorder.alpha = 0f;
        }
    }

    public void UpdateChargeRH(float currentCharge, float maxCharge)
    {
        RHSlider.maxValue = maxCharge;
        RHSlider.value = currentCharge;
    }

    public void UpdateChargeLH(float currentCharge, float maxCharge)
    {
        LHSlider.maxValue = maxCharge;
        LHSlider.value = currentCharge;
    }

    public void UpdateXP(float currentXP, float xpToNextLevel, int currentLevel)
    {
        xpText.text = currentXP + "/" + xpToNextLevel ;
        xpSlider.maxValue = xpToNextLevel;
        xpSlider.value = currentXP;
        levelText.text = currentLevel+"";
    }

    public void DisplayInformation(string text)
    {
        informationText.text = text;
        StartCoroutine(ShowText(5f, 3f));
    }
    IEnumerator ShowText(float delay, float speed)
    {
        informationTextCanvas.alpha = 1f;
        yield return new WaitForSeconds(delay);
        StartCoroutine(Decay(informationTextCanvas, speed));
    }

    public void StartDecay(CanvasGroup group)
    {
        group.alpha = 1f;
        StartCoroutine(Decay(group, 1f));
    }
    IEnumerator Decay(CanvasGroup group, float speed)
    {
        while(group.alpha > 0f)
        {
            group.alpha -= speed *Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
