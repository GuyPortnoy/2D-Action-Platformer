using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //set singular UI instance
    public static UIController instance;
    //define the health hearts in the UI
    [SerializeField] private Image heart1, heart2, heart3;
    //define the full/empty heart's sprites
    [SerializeField] private Sprite heartFull, heartEmpty, heartHalf;

    [SerializeField] private Text gemText;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;
    [SerializeField] private GameObject levelCompleteText;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    //on level start refresh the UI and fade from a black screen
    void Start()
    {
        UpdateGemCount();
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed*Time.deltaTime));
            if (fadeScreen.color.a ==1f)
            {
                shouldFadeToBlack = false;
            }

        }
        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }
    public void UpdateHealthDisplay()
    { // manage the heart display in accordance with remaining health
        switch (PlayerHealthController.instance.getCurrentHealth())
        {
            case 6:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                break;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                break;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                break;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;
        }
    }
    //changes the text in the UI to match amount collected in level
    public void UpdateGemCount()
    {
        gemText.text = LevelManager.instance.getGemsCollected().ToString();
    }
    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public float getFadeSpeed()
    {
        return fadeSpeed;
    }
    public void setLevelCompleteText(GameObject levelText)
    {
        levelCompleteText = levelText;
    }
    public GameObject getLevelCompleteText()
    {
        return levelCompleteText;
    }
}
