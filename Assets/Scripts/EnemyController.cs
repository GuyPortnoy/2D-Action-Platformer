using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime, waitTime;
    [SerializeField] private float moveCount, waitCount;

    [SerializeField] private Transform leftPoint, rightPoint;

    private bool movingRight;

    private Rigidbody2D enemyRB;
    [SerializeField] private SpriteRenderer enemySR;
    private Animator animator;
    // Start is called before the first frame update
    //on start, attach the animator and rigid body to script, remove child status from object to prevent costant 
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;
        moveCount = moveTime;

    }

    // Update is called once per frame
    void Update()
    {
        //if the move timer is going, enter this block
        if (moveCount > 0)
        {
            //count down the move timer, move model left/right depending on position
            moveCount -= Time.deltaTime;
            if (movingRight)
            {
                enemyRB.velocity = new Vector2(moveSpeed, enemyRB.velocity.y);
                enemySR.flipX = true;
                if (transform.position.x > rightPoint.position.x)
                {
                    movingRight = false;
                }
            }
            else
            {
                enemyRB.velocity = new Vector2(-moveSpeed, enemyRB.velocity.y);
                enemySR.flipX = false;
                if (transform.position.x < leftPoint.position.x)
                {
                    movingRight = true;
                }
            }
            //if wait timer is run out, set a new wait timer in a slight range
            if (moveCount<=0)
            {
                waitCount = Random.Range(waitTime*0.75f, waitTime*1.25f);

            }
            animator.SetBool("isMoving", true);
        }
        //if wait timer is ticking, remove current movement force, then set new move counter
        else if (waitCount >0)
        {
            waitCount -= Time.deltaTime;
            enemyRB.velocity = new Vector2(0, enemyRB.velocity.y);
            if (waitCount <=0)
            {
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 1.25f); ;
            }
            animator.SetBool("isMoving", false);
        }

        
    }
}
