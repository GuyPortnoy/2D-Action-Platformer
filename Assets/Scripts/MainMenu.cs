using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string startScene, continueScene;

    [SerializeField] private GameObject continueButton;
    // Start is called before the first frame update
    //on entering game, if the player has unlocked a level in the game by playing, show continue button
    void Start()
    {
        if (PlayerPrefs.HasKey(startScene+"_unlocked"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ContinueGame()
    {
        SceneManager.LoadScene(continueScene);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);

        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quitting game");
    }
}

