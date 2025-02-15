using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //func triggers when player enters collider area, refers to player and health controllers to call damage and knockback func
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("spikes hit");

            //FindObjectOfType<PlayerHealthController>().DealDamage();
            PlayerHealthController.instance.DealDamage();
            PlayerController.instance.KnockBack();
        }

        
    }
}
