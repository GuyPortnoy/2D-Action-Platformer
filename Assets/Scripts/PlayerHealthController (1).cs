using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;


    [SerializeField] private int currentHealth, maxHealth;

    [SerializeField] private float ivnincibleLength;
    private float invincibleCounter;

    private SpriteRenderer playerSpriteRenderer;

    [SerializeField] private GameObject deathEffect;
    //assigns health controller instance on game start
    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player has positive invincible count value, tick said value down and make player transparet
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;

            if (invincibleCounter <= 0)
            {
                playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r
                                            , playerSpriteRenderer.color.g, playerSpriteRenderer.color.b
                                            , 1f);
            }
        }
    }
    //if player is not in invincible state, reduce HP by one, make player immune to damage for a bit, update HP UI display
    public void DealDamage ()
    {
        if (invincibleCounter <=0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                invincibleCounter = ivnincibleLength;
                playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r
                                            , playerSpriteRenderer.color.g, playerSpriteRenderer.color.b
                                            , 0.5f);
                PlayerController.instance.KnockBack();

                AudioManager.instance.PlaySFX(9);
            }

            UIController.instance.UpdateHealthDisplay();
        }
    }
    //function created a death effect at player location, calls for respawn function from the level manager
    public void Die()
    {
        currentHealth = 0;
        //gameObject.SetActive(false);

        Instantiate(deathEffect, transform.position, transform.rotation);

        LevelManager.instance.RespawnPlayer();
    }
    //if player has less than max HP then increase by 1 and update UI
    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIController.instance.UpdateHealthDisplay();
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public void setCurrentHealth(int hp)
    {
        currentHealth = hp;
    }
    public void BeInvincible()
    {
        if (invincibleCounter <= 0)
        {
            invincibleCounter = 3;
            playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r
                                                , playerSpriteRenderer.color.g, playerSpriteRenderer.color.b
                                                , 0.5f);
        }
        
    }
}
