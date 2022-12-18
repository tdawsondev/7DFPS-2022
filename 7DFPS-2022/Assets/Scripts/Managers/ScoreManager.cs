using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;
    public Player player;

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

    //total xp gained
    public float score;

    private void Start()
    {
        xp = 0;
        score = 0;
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
        HUDManager.instance.UpdateXP(xp, score);
    }

    private void LevelUp()
    {
        MenuController.instance.ToggleLevelUp();
    }

    public void IncreaseMaxHealth()
    {
        player.health.IncreaseMaxHealth(10);
    }

    public void IncreaseMaxMana()
    {
        player.mana.IncreaseMaxMana(20);
    }

}
