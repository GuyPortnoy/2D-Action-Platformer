using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    [SerializeField] BossTankController bossCont;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //if boss is stomped call boss damage function
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="player" && PlayerController.instance.transform.position.y > transform.position.y)
        {
            bossCont.TakeHit();
            PlayerController.instance.Bounce();
            gameObject.SetActive(false);
        }
    }
}
