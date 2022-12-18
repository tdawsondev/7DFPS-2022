using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one XP Manager");
        }
        instance = this;
    }

    private float xp;
    public float xpToNextLevel = 5;
    public float manaIncrease;
    public int healthIncrease;
    public int level = 0;

    //total xp gained
    public float score;

    private void Start()
    {
        xp = 0;
        score = 0;
        level = 0;
        HUDManager.instance.UpdateXP(xp, xpToNextLevel);
    }

    //called on BaseEnemy.TookDamage, xp changes made there
    public void GainXP(float amount)
    {
        score += amount;
        xp += amount;
        if (xp >= xpToNextLevel)
        {
            xp -= xpToNextLevel;
            LevelUp();
        }
        HUDManager.instance.UpdateXP(xp, xpToNextLevel);
    }

    private void LevelUp()
    {
        level++;
        MenuController.instance.ToggleLevelUp();
    }

    public void IncreaseMaxHealth()
    {
        Player.Instance.health.IncreaseMaxHealth(healthIncrease);
        HUDManager.instance.UpdateHealth();
    }

    public void IncreaseMaxMana()
    {
        Player.Instance.mana.IncreaseMaxMana(manaIncrease);
        HUDManager.instance.UpdateManaSlider();
    }

}
