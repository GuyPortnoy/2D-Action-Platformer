using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    public static LSUIController instance;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    [SerializeField] private GameObject levelInfoPanel;
    [SerializeField] private Text levelName, gemsFound, gemsTarget,
                                    bestTime,timeTarget;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
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

    public void ShowInfo(MapPoint levelInfo)
    {
        levelName.text = levelInfo.getLevelName();
        gemsFound.text = "FOUND: " + levelInfo.getGemsCollected();
        gemsTarget.text = "IN LEVEL: " + levelInfo.getTotalGems();
        timeTarget.text = "TARGET: " + levelInfo.getTimeTarget()+"s";
        if (levelInfo.getBestTime()==0)
        {
            bestTime.text = "BEST: ---";
        }
        else
        {
            bestTime.text = levelInfo.getBestTime().ToString("F2") + "s";
        }

        levelInfoPanel.SetActive(true);
    }
    public void HideInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
