using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float respawnWait;

    [SerializeField] private int gemsCollected;

    [SerializeField] private string levelToLoad;

    private float timeInLevel;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        timeInLevel = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeInLevel += Time.deltaTime;
    }
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }
    //respawn coroutine happens outside the normal execution of unity
    //deactivates player object, calls fade screen from UI controller, spawns player back at checkpoint/initial spawn
    //remove fadescreen and play sfx
    private IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnWait - (1f/UIController.instance.getFadeSpeed()));
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController.instance.getFadeSpeed()) + .2f);
        UIController.instance.FadeFromBlack();

        PlayerController.instance.gameObject.SetActive(true);

        PlayerController.instance.transform.position = CheckpointController.instance.getSpawnPoint();

        PlayerHealthController.instance.setCurrentHealth(PlayerHealthController.instance.getMaxHealth());

        UIController.instance.UpdateHealthDisplay();

        AudioManager.instance.PlaySFX(8);


    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }
    //func calls end level, stops player input and halts camera tracking
    //shows win text, fades to black
    public IEnumerator EndLevelCo()
    {
        AudioManager.instance.PlayLevelVictory();

        PlayerController.instance.SetStopInput(true);

        CameraController.instance.setStopFollow(true);

        UIController.instance.getLevelCompleteText().SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.instance.getFadeSpeed()) + 3f);
        //unlock the level for use from the level select
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        //save only the highest amount of gems collected in a level
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if (gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }

        }
        //save gems collected if no previous stats are present
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
        }
        //save only the fastest time in which player completes a level
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if (timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }

        }
        //save time spent in level if no previous stats are present
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }

        SceneManager.LoadScene(levelToLoad);
    }

    public int getGemsCollected()
    {
        return gemsCollected;
    }
    public void setGemsCollected(int gems)
    {
        gemsCollected = gems;
    }
}
