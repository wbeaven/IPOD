using Ami.BroAudio;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider, sensSlider;
    [SerializeField] Toggle mouseXTog, mouseYTog, moveXTog, moveYTog;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] PlayerMovement[] playerMovement;
    [SerializeField] SoundID buttonSFX = default;

    static float sensitivity = 100f, volume = 0.7f;
    static bool mouseX, mouseY, moveX, moveY;

    public void GetVolume(float sliderVolume)
    {
        BroAudio.Play(buttonSFX);
        volume = sliderVolume;
    }

    public void GetSens(float sliderSens)
    {
        BroAudio.Play(buttonSFX);
        sensitivity = sliderSens;
        if (mouseLook != null)
            mouseLook.mouseSens = sensitivity;
    }

    public void InvertLookX()
    {
        mouseX = mouseXTog.isOn;
        if (mouseLook != null)
            mouseLook.invertX = mouseX;
        BroAudio.Play(buttonSFX);
    }

    public void InvertLookY()
    {
        mouseY = mouseYTog.isOn;
        if (mouseLook != null)
            mouseLook.invertY = mouseY;
        BroAudio.Play(buttonSFX);
    }

    public void InvertMoveX()
    {
        moveX = moveXTog.isOn;
        if (playerMovement[0] != null)
            playerMovement[0].invertX = moveX;
        BroAudio.Play(buttonSFX);
    }

    public void InvertMoveY()
    {
        moveY = moveYTog.isOn;
        if (playerMovement[1] != null)
            playerMovement[1].invertY = moveY;
        BroAudio.Play(buttonSFX);
    }

    private void Start()
    {
        volumeSlider.value = volume;
        sensSlider.value = sensitivity;
        mouseXTog.isOn = mouseX;
        mouseYTog.isOn = mouseY;
        moveXTog.isOn = moveX;
        moveYTog.isOn = moveY;
    }
}