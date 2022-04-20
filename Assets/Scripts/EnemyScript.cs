using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var pos = transform.localPosition;
        pos.x += speed * Time.deltaTime;
        transform.localPosition = pos;
        //flip();
        //rb.velocity = new Vector2(speed, rb.velocity.y);
        //rotate facing of enemy
        if (speed > 0f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (speed < 0f)
        {
            transform.localScale = Vector2.one;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gap"))
        {
            speed = -speed;            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            //collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            speed = -speed;

            //rotate facing of enemy
            if (speed > 0f)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else if (speed < 0f)
            {
                transform.localScale = Vector2.one;
            }
        }
    }
}
