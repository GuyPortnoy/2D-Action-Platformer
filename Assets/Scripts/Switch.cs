using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject objectToSwitch;
    [SerializeField] private SpriteRenderer theSR;
    [SerializeField] private Sprite downSprite;
    private bool hasSwitched;
    [SerializeField] bool deactivateOnSwitch;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //on player entering switch collider, make the attached object appear/disappear
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasSwitched)
        {
            if (deactivateOnSwitch)
            {
                objectToSwitch.SetActive(false);
            }
            else
            {
                objectToSwitch.SetActive(true);
            }
            
            theSR.sprite = downSprite;
            hasSwitched = true;
        }
    }
}
