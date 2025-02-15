using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private MapPoint left, right;
    [SerializeField] private bool isLevel, isLocked;
    [SerializeField] private string levelToLoad, levelToCheck, levelName;
    [SerializeField] private GameObject gemBadge, timeBadge;
    [SerializeField] private int gemsCollected, totalGems;
    [SerializeField] private float bestTime, targetTime;

    // Start is called before the first frame update
    void Start()
    {
        //read out the time and gems stats collected during play
        //reads from the playerpref file
        if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
        {
            gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
        }
        if (PlayerPrefs.HasKey(levelToLoad + "_time"))
        {
            bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
        }
        if(gemsCollected >= totalGems)
        {
            gemBadge.SetActive(true);
        }
        if (bestTime <= targetTime && bestTime!=0)
        {
            timeBadge.SetActive(true);
        }
        //if the point is a level point with a level attached turn to this
        if (isLevel && levelToLoad != null)
        {
            isLocked = true;
            if (levelToCheck!=null)
            {
                if(PlayerPrefs.HasKey(levelToCheck+"_unlocked"))
                {
                    if(PlayerPrefs.GetInt(levelToCheck + "_unlocked")==1)
                    {
                        isLocked = false;
                    }
                    
                }
            }
            if (levelToLoad == levelToCheck)
            {
                isLocked = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MapPoint getLeft()
    {
        return left;
    }
    public MapPoint getRight()
    {
        return right;
    }
    public bool getIsLevel()
    {
        return isLevel;
    }
    public string getLevelToLoad()
    {
        return levelToLoad;
    }
    public bool getIsLocked()
    {
        return isLocked;
    }
    public string getLevelName()
    {
        return levelName;
    }
    public int getGemsCollected()
    {
        return gemsCollected;
    }
    public int getTotalGems()
    {
        return totalGems;
    }
    public float getBestTime()
    {
        return bestTime;
    }
    public float getTimeTarget()
    {
        return targetTime;
    }
}
