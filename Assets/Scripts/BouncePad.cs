using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float bounceForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.getPlayerRB().velocity
                = new Vector2(PlayerController.instance.getPlayerRB().velocity.x,
                bounceForce);

            animator.SetTrigger("Bounce");
        }
    }
}
