using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [SerializeField] private string levelSelect, mainMenu;

    public GameObject pauseScreen;

    [SerializeField] private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    //if at any point the player presses ESC, pring up or take off the pause menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }
    //turn pause menu on/off, if turned on halts timescale, which stops all time related functions
    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    //use built in scene manager to load into the main menu when pressing the main menu button
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
    //change scene to the level select scene
    public void LevelSelect()
    {
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;
    }

    public bool getPauseState()
    {
        return isPaused;
    }
}
