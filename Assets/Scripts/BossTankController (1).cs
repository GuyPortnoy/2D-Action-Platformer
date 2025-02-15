using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    [SerializeField] private enum bossStates { shooting, hurt, moving, ended};
    [SerializeField] private bossStates currentState;

    [SerializeField] private Transform theBoss,leftPoint,rightPoint,firePoint,minePoint;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed,timeBetweenShots,hurtTime,timeBetweenMines, shotSpeedUp, mineSpeedUp;
    private bool moveRight,isDefeated;
    private float shotCounter,hurtCounter,mineCounter;
    [SerializeField] private GameObject bullet, hitBox, mine, explosion;
    [SerializeField] private int bossHealth=5;
    [SerializeField] GameObject bossExit;
    // Start is called before the first frame update
    //initial state for the boss is shooing
    void Start()
    {
        currentState = bossStates.shooting;
    }

    // Update is called once per frame
    void Update()
    {
        //tank boss state machine
        switch(currentState)
        {
            //shooting state, boss with instantiate bullets on a constant timer
            case bossStates.shooting:
                shotCounter -= Time.deltaTime;
                if (shotCounter<=0)
                {
                    shotCounter = timeBetweenShots;
                    var newBullet= Instantiate(bullet, firePoint.position, firePoint.rotation);
                    newBullet.transform.localScale = theBoss.localScale;
                }
                break;
                //hurt state, plays a hurt animation
            case bossStates.hurt:
                if (hurtCounter>0)
                {
                    hurtCounter -= Time.deltaTime;
                    if (hurtCounter<=0)
                    {
                        currentState = bossStates.moving;
                        mineCounter = 0;
                        //if boss is defeated (0 HP, thus a true defeat bool) turn on arena exit, remove boss, change to end state
                        if (isDefeated)
                        {
                            bossExit.SetActive(true);
                            theBoss.gameObject.SetActive(false);
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            currentState = bossStates.ended;
                        }
                    }
                }
                break;
                //movement state, boss will move to the direction opposite his current point (left/right)
            case bossStates.moving:
                if (moveRight)
                {
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime,
                        0f, 0f);
                    if (theBoss.position.x > rightPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(1f, 1f, 1f);
                        moveRight = false;
                        EndMovement();
                    }
                    
                }
                else
                {
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime,
                    0f, 0f);
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        moveRight = true;
                        EndMovement();
                    }
                }
                mineCounter -= Time.deltaTime;
                if (mineCounter <= 0)
                {
                    mineCounter = timeBetweenMines;
                    Instantiate(mine, minePoint.position, minePoint.rotation);
                }
                break;
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
            TakeHit();
#endif
    }
    //function changes boss state to hurt, triggers hurt animation, destroys any mines previously laid out
    public void TakeHit()
    {
        currentState = bossStates.hurt;
        hurtCounter = hurtTime;
        animator.SetTrigger("Hit");
        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if (mines.Length>0)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }
        bossHealth--;
        if (bossHealth <= 0)
        {
            isDefeated = true;
        }
        else
        {
            timeBetweenShots /= shotSpeedUp;
            timeBetweenMines /= mineSpeedUp;
        }
    }
    //changes boss state to shooting, halts movement and enables the hitbox again
    private void EndMovement()
    {
        currentState = bossStates.shooting;
        shotCounter = 0f;
        animator.SetTrigger("StopMoving");
        hitBox.SetActive(true);
    }
}
