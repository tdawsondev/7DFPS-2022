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


    public delegate void OnLevelUp(int newLevel);

    public event OnLevelUp LeveledUp;

    private void Start()
    {
        xp = 0;
        score = 0;
        level = 0;
        HUDManager.instance.UpdateXP(xp, xpToNextLevel, level);
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
        HUDManager.instance.UpdateXP(xp, xpToNextLevel, level);
    }

    private void LevelUp()
    {
        level++;
        if(level == 5 || level == 10)
        {
            Player.Instance.magicSystem.DamageBounus++;
        }
        MenuController.instance.ToggleLevelUp(level);
        xpToNextLevel = Mathf.Round((xpToNextLevel*1.25f) / 5f) * 5;
        GainXP(0);
        if(LeveledUp != null)
        {
            LeveledUp(level);
        }
        //IncreaseNumberOfEnemies
        if(level % 2 == 0)
        {
            SpawnManager.instance.targetSpawnCount++;
        }
        
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
