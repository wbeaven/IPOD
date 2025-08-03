using Ami.BroAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsUI;
    [SerializeField] PlayerSettings playerSettings;
    [SerializeField] PlayableDirector director;
    [SerializeField] SoundID menuBGM = default, warpBGM = default, buttonSFX = default;

    bool settingsOpen;

    private void Start()
    {
        BroAudio.Play(menuBGM);
    }

    public void Play()
    {
        BroAudio.Play(buttonSFX);
        director.Play();
    }

    public void WarpMusic()
    {
        BroAudio.Play(warpBGM);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        BroAudio.Play(buttonSFX);
        if (!settingsOpen)
        {
            settingsUI.SetActive(true);
            settingsOpen = true;
        }
        else
        {
            settingsUI.SetActive(false);
            settingsOpen = false;
        }
    }

    public void Quit()
    {
        BroAudio.Play(buttonSFX);
        Application.Quit();
    }
}
