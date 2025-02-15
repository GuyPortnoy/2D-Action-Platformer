using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private bool isGem,isHeal;

    private bool isCollected;

    [SerializeField] private GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //func activates on player entering collider area
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")&&!isCollected)
        {
            //if pickup is a gem, remove gem from screen, add one to gem count in UI, play pickup sound
            if (isGem)
            {
                LevelManager.instance.setGemsCollected(LevelManager.instance.getGemsCollected()+1);
                isCollected = true;
                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                UIController.instance.UpdateGemCount();

                AudioManager.instance.PlaySFX(6);
            }
            //if pickup is a health pikcup, remove object from level, add 1 health to player, play SFX
            if (isHeal)
            {
                if (PlayerHealthController.instance.getCurrentHealth() != PlayerHealthController.instance.getMaxHealth())
                {
                    PlayerHealthController.instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    AudioManager.instance.PlaySFX(7);
                }
            }
        }
    }
}
