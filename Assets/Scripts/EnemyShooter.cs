using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet,leftPT,rightPT;
    [SerializeField] private Transform bulletPos;
    private Rigidbody2D rb;
    private GameObject player;
    private Transform currentPoint;
    private Animator animator;
    private float firingTimer;
    [SerializeField] private float fireRange,moveSpeed;
    // Start is called before the first frame update
    //on start remove child status from patrol points, set moving animation and find player object to target
    void Start()
    {
        leftPT.transform.parent = null;
        rightPT.transform.parent = null;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        currentPoint = rightPT.transform;
        animator.SetBool("isRunning", true);
        
    }

    // Update is called once per frame
    //
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == rightPT.transform)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
        if (Vector2.Distance(transform.position,currentPoint.position)
            <1f && currentPoint == rightPT.transform)
        {
            Flip();
            currentPoint = leftPT.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position)
            < 1f && currentPoint == leftPT.transform)
        {
            Flip();
            currentPoint = rightPT.transform;
        }

        float distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceFromPlayer<fireRange)
        {
            firingTimer += Time.deltaTime;
            if (firingTimer > 2)
            {
                firingTimer = 0;
                Shoot();
            }
        }

        
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    public void Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
