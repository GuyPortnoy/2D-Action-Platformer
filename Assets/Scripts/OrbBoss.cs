using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBoss : MonoBehaviour
{
    [SerializeField] private Transform bossSprite;
    [SerializeField] private Transform leftPoint, rightPoint, bulletPos;
    private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int bossHealth = 6;
    [SerializeField] private GameObject explosion, bullet,
        leftPlatforms,rightPlatforms,sawTraps,smashers;
    private float shotTimer;
    private GameObject player;
    private bool inLeftPhase;
    [SerializeField] GameObject bossExit;
    // Start is called before the first frame update
    void Start()
    {
        bossExit.SetActive(false);
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        leftPoint.parent = null;
        rightPoint.parent = null;
        inLeftPhase = false;
        sawTraps.SetActive(false);
        smashers.SetActive(false);
        shotTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //boss is docile until hit, once hit activate the below state machine
        if (bossHealth<6)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > 2)
            {
                shotTimer = 0;
                Shoot();
            }
            //boss changes the side of the arena depending on odd/even HP
            if (bossHealth % 2 == 0)
            {
                BossPhaseLeft();
            }
            else if (bossHealth % 2 != 0)
            {
                BossPhaseRight();
            }
        }
        //activate saws when boss reaches 4HP
        if (bossHealth==4)
        {
            sawTraps.SetActive(true);
        }
        //activate smashers when boss reaches 2HP
        if (bossHealth==2)
        {
            smashers.SetActive(true);
        }
        if (bossHealth<=0)
        {
            Die();
        }



        
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
            TakeHit();
#endif
    }
    //move boss to left side of arena, activate only left platforms
    public void BossPhaseLeft()
    {
        bossSprite.position = Vector3.MoveTowards(bossSprite.position,
            leftPoint.position, moveSpeed * Time.deltaTime);
        rightPlatforms.SetActive(false);
        leftPlatforms.SetActive(true);
        inLeftPhase = true;
    }
    //move boss to right side of arena, activate only right platforms
    public void BossPhaseRight()
    {
        bossSprite.position = Vector3.MoveTowards(bossSprite.position,
            rightPoint.position, moveSpeed * Time.deltaTime);
        leftPlatforms.SetActive(false);
        rightPlatforms.SetActive(true);
        inLeftPhase = false;
    }
    //take 1 int off the boss current health, if reacning 0 disable traps
    //and platforms, then call death function
    public void TakeHit()
    {
        if (bossHealth>0)
        {
            bossHealth--;
        }
        else if (bossHealth<=0)
        {
            sawTraps.SetActive(false);
            smashers.SetActive(false);
            leftPlatforms.SetActive(false);
            rightPlatforms.SetActive(false);
            Die();
        }
    }
    //activates the exit from the boss arena, create explosion, turn off boss object
    public void Die()
    {
        bossExit.SetActive(true);
        Instantiate(explosion, bossSprite.position,bossSprite.rotation);
        gameObject.SetActive(false);
    }
    public void Shoot()
    {
        Instantiate(bullet, bossSprite.position, Quaternion.identity);
    }
    public void SpawnSaws()
    {
        sawTraps.SetActive(true);
    }
    public void SpawnSmashers()
    {
        smashers.SetActive(true);
    }
}
