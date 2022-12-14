using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one MenuController");
        }
        instance = this;
    }


    public GameObject pauseMenu;
    public bool paused = false;
    public Animator pauseAnimator;
    public Animator fadeAnimator;
    public CanvasGroup settingsCanvas;
    public GameObject gameOverMenu;
    public bool cantPause = false;
    public GameObject levelUpMenu;
    public TMP_Text levelMenuText;
    public GameObject DamageIncreaseText;
    public TMP_Text gameOverLevelText;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(IEOnLoadIn());
        if (pauseMenu)
            pauseMenu.SetActive(false);
        if(gameOverMenu)
            gameOverMenu.SetActive(false);

        if(DamageIncreaseText)
            DamageIncreaseText.SetActive(false);

    }

    void Update()
    {
        if (pauseMenu && InputManager.Instance.PausePressed())
        {
            TogglePause();
        }
    }
    public void Freeze()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Player.Instance.LockPlayer();
    }
    public void UnFreeze()
    {
        Player.Instance.UnlockPlayer();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }


    public void TogglePause()
    {
        if (pauseMenu == null ||cantPause) return;
        if (!paused)
        {
            paused = true;
            pauseMenu.SetActive(true);
            Freeze();
            pauseAnimator.SetTrigger("FadeIn");
        }
        else
        {
            StartCoroutine(IEUnpause());
        }
    }
    private IEnumerator IEUnpause()
    {
        settingsCanvas.alpha = 0f;
        settingsCanvas.interactable = false;
        settingsCanvas.blocksRaycasts = false;
        pauseAnimator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.15f);
        paused = false;
        pauseMenu.SetActive(false);
        UnFreeze();

    }
    public void CloseApplication()
    {
        Application.Quit();
    }

    public void ReloadLevel()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadScene(int buildIndex)
    {
        StartCoroutine(IELoadScene(buildIndex));
    }

    public IEnumerator IEOnLoadIn()
    {
        fadeAnimator.gameObject.SetActive(true);
        fadeAnimator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.3f);
        fadeAnimator.gameObject.SetActive(false);
    }
    public IEnumerator IELoadScene(int buildIndex)
    {
        fadeAnimator.gameObject.SetActive(true);
        fadeAnimator.SetTrigger("FadeIn");
        yield return new WaitForSecondsRealtime(0.3f);
        if (AudioManager.instance.GetClip("Recharge").isPlaying)
        {
            AudioManager.instance.StopSound("Recharge");
            AudioManager.instance.Play("MainTheme");
        }
        SceneManager.LoadScene(buildIndex);

    }
    public void OpenGameOverMenu()
    {
        Freeze();
        AudioManager.instance.Play("GameOver");
        cantPause = true;
        gameOverLevelText.text = "You made it to level " + ScoreManager.instance.level + "!";
        gameOverMenu.SetActive(true);
    }

    public void ToggleLevelUp(int newLevel)
    {
        if (levelUpMenu == null || cantPause) return;
        if (!paused)
        {
            paused = true;
            levelMenuText.text = "You reached level: "+newLevel;
            if(newLevel == 5 || newLevel == 10)
            {
                DamageIncreaseText.SetActive(true);
            }
            else
            {
                DamageIncreaseText.SetActive(false);
            }
            AudioManager.instance.Play("Stinger");
            levelUpMenu.SetActive(true);
            Freeze();
        }
        else
        {
            StartCoroutine(IEUnLevelPause());
        }
    }
    private IEnumerator IEUnLevelPause()
    {
        //add animator effects
        yield return new WaitForSecondsRealtime(0.15f);
        paused = false;
        levelUpMenu.SetActive(false);
        UnFreeze();
    }

    public void PlaySound(string sound)
    {
        AudioManager.instance.Play(sound);
    }

    public void IncreaseMaxHealth()
    {
        ScoreManager.instance.IncreaseMaxHealth();
    }

    public void IncreaseMaxMana()
    {
        ScoreManager.instance.IncreaseMaxMana();
    }

}
