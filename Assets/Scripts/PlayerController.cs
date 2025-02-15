using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // singleton instancing for player character
    public static PlayerController instance;
    //variables for vertical and horizontal movement
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private bool isGrounded, canDash=true,isDashing;
    [SerializeField] private float bounceForce;

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Transform groundCheckPoint, attackPoint,rightAttackPT;
    [SerializeField] private LayerMask whatIsGround;
    private Animator animator;
    private SpriteRenderer privateSR;
    //variables for knockback in the event of damage to player
    [SerializeField] private float knockBackLength, knockBackForce, attackRange = 0.5f, slowLength;
    private float knockBackCounter,slowCounter;
    [SerializeField] private bool stopInput,aimingLeft;
    [SerializeField] private float dashingPower,dashingTime,dashingCooldown;
    [SerializeField] private float slowedSpeed;

    private bool canDoubleJump;
    //upon starting the game, assign the player singleton
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        privateSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player isnt in a pause menu and isnt forced to stop input proceed
        if (!PauseMenu.instance.getPauseState() && ! stopInput)
        {
            //controls active for when the player isnt being knocked back
            if (knockBackCounter <= 0)
            {
                //move horizontally
                if (slowCounter<=0)
                {
                    playerRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), playerRB.velocity.y);
                }
                else
                {
                    slowCounter -= Time.deltaTime;
                    playerRB.velocity = new Vector2(slowedSpeed * Input.GetAxis("Horizontal"), playerRB.velocity.y);
                }
                //determine grounded state and reset double jump charge
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
                if (isGrounded)
                    canDoubleJump = true;
                //jump
                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                            canDoubleJump = false;
                            AudioManager.instance.PlaySFX(10);
                        }

                    }
                }
                //call attack function
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Attack();
                }
                //call invincibility function (found in the health controller)
                if (Input.GetKeyDown(KeyCode.X))
                {
                    PlayerHealthController.instance.BeInvincible();
                }
                //call dash function
                if (Input.GetKeyDown(KeyCode.C))
                {
                    StartCoroutine(Dash());
                }

                if (playerRB.velocity.x < 0&&transform.localScale.x>0)
                {
                    FlipPlayer();
                }
                else if (playerRB.velocity.x > 0 && transform.localScale.x < 0)
                {
                    FlipPlayer();
                }
                
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (transform.localScale.x < 0)
                {
                    playerRB.velocity = new Vector2(-knockBackForce, playerRB.velocity.y);
                }
                else
                {
                    playerRB.velocity = new Vector2(knockBackForce, playerRB.velocity.y);
                }
            }
        }
        
        //set animations for run and jump
        animator.SetFloat("moveSpeed", Mathf.Abs(playerRB.velocity.x));
        animator.SetBool("isGrounded", isGrounded);


    }
    public void SlowPlayer()
    {
        slowCounter = slowLength;
    }
    //player's attack function, on button press an area is created in front
    //of the player, where all entities are put in an array, if it has enemies
    //in said array, react accordingly (instakill regulars and hurt bosses)
    public void Attack()
    {
        //play animation
        animator.SetTrigger("isAttacking");
        //detect enemies in hit area
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange);
        //hurt enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.transform.gameObject.SetActive(false);
            }
            else if (enemy.tag == "SlowField")
            {
                enemy.transform.parent.gameObject.SetActive(false);
            }
            else if (enemy.tag == "HopperBoss")
            {
                enemy.gameObject.GetComponent<HopperBoss>().TakeHit();
            }
            else if (enemy.tag == "OrbBoss")
            {
                enemy.gameObject.GetComponentInParent<OrbBoss>().TakeHit();
            }
            else if (enemy.tag == "TankBoss")
            {
                enemy.GetComponentInParent<BossTankController>().TakeHit();
            }
        }
    }
    //set player x scale to reverse from current value, allows child elements to flip with the player
    public void FlipPlayer()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    //function sets a knockback force for a duration, triggers animation
    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        playerRB.velocity = new Vector2(0f, playerRB.velocity.y);

        animator.SetTrigger("hurt");
    }
    //applies an upwards force and plays a sound effect
    public void Bounce()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
    
    public void SetStopInput(bool stopStatus)
    {
        stopInput = stopStatus;
    }
    public bool GetStopInput()
    {
        return stopInput;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = other.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            transform.parent = null;
        }
    }
    public Rigidbody2D getPlayerRB()
    {
        return playerRB;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    //halts player vertical movement and applies a new movespeed value for dash duration
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRB.gravityScale;
        playerRB.gravityScale = 0f;
        playerRB.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        playerRB.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
}
