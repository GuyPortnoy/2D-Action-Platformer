using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider2d;
    private Animator animator;
    [SerializeField] private LayerMask groudlayer;
    [SerializeField] private LayerMask walllayer;
    private float wallJampCooldown;
    private float horizontal;
    //[SerializeField] private LayerMask platformslayermask;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0.1f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("running", horizontal != 0);
        animator.SetBool("grounded", isGrounded());

        if (wallJampCooldown > 0.2f)
        {
           
            body.velocity = new Vector2(horizontal * moveSpeed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 2;
            if (Input.GetKey(KeyCode.Space))
                Jump();

        }
        else
            wallJampCooldown += Time.deltaTime;

    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetTrigger("jumping");
        }
        else if (onWall() && !isGrounded())
        {
            if(horizontal == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, 1);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.x);
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJampCooldown = 0;
            
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0, Vector2.down, 0.1f, groudlayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, walllayer);
        return raycastHit.collider != null;
    }

}
