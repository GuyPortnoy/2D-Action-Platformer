using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    [SerializeField] private LSPlayer thePlayer;

    private MapPoint[] allPoints;
    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint point in allPoints)
            {
                if (point.getLevelToLoad()==PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = point.transform.position;
                    thePlayer.setCurrentPoint(point);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }
    public IEnumerator LoadLevelCo()
    {
        LSUIController.instance.FadeToBlack();

        yield return new WaitForSeconds(1f/LSUIController.instance.getFadeSpeed()+.25f);

        SceneManager.LoadScene(thePlayer.getCurrentPoint().getLevelToLoad());
    }
}
