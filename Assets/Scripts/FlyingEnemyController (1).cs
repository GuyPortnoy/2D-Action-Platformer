using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float moveSpeed, distanceToAttackPlayer,chaseSpeed;
    [SerializeField] private int currentPoint;

    [SerializeField] private SpriteRenderer enemySR;
    private Vector3 attackTarget;
    private bool hasAttacked;
    [SerializeField] private float waitAfterAttack, attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0;i<points.Length;i++)
        {
            points[i].parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCounter>0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position
                ) > distanceToAttackPlayer)
            {
                attackTarget = Vector3.zero;
                transform.position = Vector3.MoveTowards(transform.position,
                            points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, points[currentPoint].position)
                    < 0.5f)
                {
                    currentPoint++;

                    if (currentPoint >= points.Length)
                    {
                        currentPoint = 0;
                    }
                }
                if (transform.position.x < points[currentPoint].position.x)
                {
                    enemySR.flipX = true;
                }
                else if (transform.position.x > points[currentPoint].position.x)
                {
                    enemySR.flipX = false;
                }
            }
            else
            {
                //attacking the player
                if (attackTarget == Vector3.zero)
                {
                    attackTarget = PlayerController.instance.transform.position;
                }
                transform.position = Vector3.MoveTowards
                    (transform.position,
                    attackTarget, chaseSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, attackTarget) <= .1f)
                {
                    hasAttacked = true;
                    attackCounter = waitAfterAttack;
                    attackTarget = Vector3.zero;
                }
            }
        }

        
    }
}
