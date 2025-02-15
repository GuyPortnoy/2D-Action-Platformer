using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float speed;
    private float timer;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        float rotation = Mathf.Atan2(-direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation+180);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >3)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Player")
        {
            PlayerHealthController.instance.DealDamage();
        }
        
    }
}
