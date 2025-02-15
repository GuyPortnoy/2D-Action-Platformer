using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperBoss : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform leftPoint, rightPoint;
    private bool movingRight;
    private Rigidbody2D bossRB;
    [SerializeField] private SpriteRenderer bossSR;
    [SerializeField] private float moveTime, waitTime, waitRecudtion, speedUp;
    private float moveCount, waitCount;
    private Animator animator;
    [SerializeField] private int bossHealth=4 ;
    [SerializeField] private GameObject explosion,bullet,bossExit;
    private float shotTimer;
    private GameObject player;

    void Start()
    {
        bossRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;
        moveCount = moveTime;
        bossExit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //block covering boss movement
        if (moveCount>0)
        {
            //boss moving right
            moveCount -= Time.deltaTime;
            if (movingRight)
            {
                bossRB.velocity = new Vector2(moveSpeed, bossRB.velocity.y);
                bossSR.flipX = false;
                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;

                }

            }
            //boss moving left
            else
            {
                bossRB.velocity = new Vector2(-moveSpeed, bossRB.velocity.y);
                bossSR.flipX = true;
                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;

                }
            }
            //after the move timer runs out, set time for boss to wait in place
            if (moveCount <=0)
            {
                waitCount = Random.Range(waitTime*0.75f,waitTime*1.25f);
            }
            animator.SetBool("isMoving", true);
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            bossRB.velocity = new Vector2(0f, bossRB.velocity.y);
            //when wait timer runs out, set a move timer to allow boss movement
            if (waitCount <=0)
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 1.25f);
            }
            animator.SetBool("isMoving", false);
        }
        //when boss reaches half-health turn on projectile firing
        if (bossHealth<=3)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > 2.5)
            {
                shotTimer = 0;
                Shoot();
            }
        }
        else if (bossHealth<=0)
        {
            Die();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.G))
            TakeHit();
#endif

    }
    //every time the boss takes a hit, increase movement speed and reduce wait time
    public void TakeHit()
    {
        bossHealth--;
        //when boss HP reaches zero, call die func
        if (bossHealth <= 0)
        {
            Die();
        }
        else
        {
            waitTime -= waitRecudtion;
            moveSpeed += speedUp;
        }
    }
    //disable the boss object, turn on boss arena exit
    public void Die()
    {
        bossExit.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Shoot()
    {
        Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
    }

}
