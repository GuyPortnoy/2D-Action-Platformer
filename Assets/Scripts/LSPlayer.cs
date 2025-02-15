using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{

    [SerializeField] private MapPoint currentPoint;
    [SerializeField] private float moveSpeed = 10f;
    private bool levelLoading;
    [SerializeField] private LSManager theLSManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentPoint.transform.position)
            < .1f && !levelLoading)
        {
            if (Input.GetAxisRaw("Horizontal") > .5f)
            {
                if (currentPoint.getRight() != null)
                {
                    SetNextPoint(currentPoint.getRight());
                }
            }
            if (Input.GetAxisRaw("Horizontal") < -.5f)
            {
                if (currentPoint.getLeft() != null)
                {
                    SetNextPoint(currentPoint.getLeft());
                }
            }

            if (currentPoint.getIsLevel() && currentPoint.getLevelToLoad()!=""
                && !currentPoint.getIsLocked())
            {
                LSUIController.instance.ShowInfo(currentPoint);

                if (Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;
                    theLSManager.LoadLevel();
                }
            }
        }

        
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        LSUIController.instance.HideInfo();
    }

    public MapPoint getCurrentPoint()
    {
        return currentPoint;
    }
    public void setCurrentPoint(MapPoint point)
    {
        currentPoint = point;
    }
}
