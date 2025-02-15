using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject collectible;

    [Range(0,100)] [SerializeField] private float chanceToDrop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //function turns on when enemy enters it's trigger area, instantly kills and has a chance to spawn healing
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy"))
        {
            Debug.Log("Stomped Enemy");
            other.transform.parent.gameObject.SetActive(false);
            //other.transform.gameObject.SetActive(false);
            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            PlayerController.instance.Bounce();
            float dropSelector = Random.Range(0,100f);

            if (dropSelector <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }

            AudioManager.instance.PlaySFX(3);
        }
    }
}
