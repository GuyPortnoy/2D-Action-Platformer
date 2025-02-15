using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Transform followTarget;

    [SerializeField] private Transform farBackGround , middleBackGround;

    [SerializeField] private float minHeight, maxHeight;

    [SerializeField] private bool stopFollow;

    //private float lastXPos;
    private Vector2 lastPos;
    // Start is called before the first frame update
    //assing singleton
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.position = new Vector3(followTarget.position.x,followTarget.position.y,transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y,minHeight,maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);*/

        //camera parallax with a Y axis limitation
        if (!stopFollow)
        {
            transform.position = new Vector3(followTarget.position.x,
            Mathf.Clamp(followTarget.position.y, minHeight, maxHeight), transform.position.z);

            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            farBackGround.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackGround.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

            lastPos = transform.position;
        }
        
    }
    public bool getStopFollow()
    {
        return stopFollow;
    }
    public void setStopFollow(bool stopStatus)
    {
        stopFollow = stopStatus;
    }
}
