using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu, settingsMenu;
    [SerializeField] private AudioMixer mainMixer;
    //close the main menu and show the setting window
    public void ShowSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    //return from the settings panel to the regular main menu
    public void ExitSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    //SETTINGS PANEL FUNCTIONS
    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
