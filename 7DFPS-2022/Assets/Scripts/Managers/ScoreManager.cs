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
    public float xpToNextLevel = 2;

    public float score;

    private void Start()
    {
        xp = 0;
        score = 0;
    }

    public void GainXP(float amount)
    {
        score += amount;
        xp += amount;
        if (xp >= xpToNextLevel)
        {
            xp -= xpToNextLevel;
            LevelUp();
        }
        HUDManager.instance.UpdateXP(xp);
    }

    private void LevelUp()
    {

    }

}
