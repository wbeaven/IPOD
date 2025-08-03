using Ami.BroAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsUI, lowHealthUI, gameOverUI, winUI, transitionPanel, player;
    [SerializeField] SoundID gameBGM = default, buttonSFX = default;

    MouseLook mouseLook;
    Shooting shooting;

    bool settingsOpen;

    void Start()
    {
        mouseLook = player.GetComponent<MouseLook>();
        shooting = player.GetComponent<Shooting>();
        BroAudio.Play(gameBGM);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings();
        }
    }

    public void Settings()
    {
        BroAudio.Play(buttonSFX);
        if (!settingsOpen)
        {
            settingsUI.SetActive(true);
            settingsOpen = true;
            mouseLook.UnlockMouse();
            Time.timeScale = 0f;
            shooting.paused = true;
        }
        else
        {
            settingsUI.SetActive(false);
            settingsOpen = false;
            mouseLook.LockMouse();
            Time.timeScale = 1f;
            shooting.paused = false;
        }
    }

    public void MenuReturn()
    {
        BroAudio.Play(buttonSFX);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void LowHealth()
    {
        lowHealthUI.SetActive(true);
    }

    public void GameOver()
    {
        lowHealthUI.SetActive(false);
        StartCoroutine(GameOverSlowmo());
    }

    private IEnumerator GameOverSlowmo()
    {
        Time.timeScale = 0.5f;
        gameOverUI.SetActive(true);
        shooting.enabled = false;
        yield return new WaitForSeconds(3f);
        Time.timeScale = 1f;
        transitionPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        mouseLook.UnlockMouse();
        SceneManager.LoadScene(0);
    }

    public void Win()
    {
        StartCoroutine(WinState());
    }

    private IEnumerator WinState()
    {
        shooting.enabled = false;
        yield return new WaitForSeconds(5f);
        winUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        transitionPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        mouseLook.UnlockMouse();
        SceneManager.LoadScene(0);
    }
}
