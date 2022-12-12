using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused = false;
    public Animator pauseAnimator;
    public Animator fadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(IEOnLoadIn());
        if (pauseMenu)
            pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (pauseMenu && InputManager.Instance.PausePressed())
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (pauseMenu == null) return;
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Player.Instance.LockPlayer();
            pauseAnimator.SetTrigger("FadeIn");
        }
        else
        {
            StartCoroutine(IEUnpause());
        }
    }
    private IEnumerator IEUnpause()
    {
        pauseAnimator.SetTrigger("FadeOut");
        yield return new WaitForSecondsRealtime(0.15f);
        paused = false;
        pauseMenu.SetActive(false);
        Player.Instance.UnlockPlayer();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

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
        SceneManager.LoadScene(buildIndex);

    }
}
