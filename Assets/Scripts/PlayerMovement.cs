using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    private float horizontalInput;

    //variable to show if player has just fallen to the ground
    private bool jumpFallen;

    private AudioSource audioSource;

    public static int score = 0;

    public static int lives = 3;

    private Vector2 position;

    private bool isJumping;

    //the time the Space key is pressed
    private float jumpTime;

    //maximum time for holding Space button having effects
    private float buttonTime = 0.3f;

    //amount of force to pull player down
    private float cancelRate = 100;

    private bool jumpCancelled;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        position = transform.position;
    }

    private void FixedUpdate()
    {
        if(jumpCancelled && isJumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
        animator.SetBool("IsGrounded", isGrounded());

        if (transform.position.y <= -8f)
        {
            kill();
        }

        if (!isGrounded() && rb.velocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false);
        }

        Move();
    }

    private void Update()
    {
        jump();
    }

    private void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //rotate player left or right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        //move character
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        animator.SetBool("Running", horizontalInput != 0);
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            jumpTime = 0;
            jumpCancelled = false;
            audioSource.time = 0.4f;
            audioSource.Play();
            animator.SetTrigger("Jump");
        }
        if (isJumping)
        {
            jumpTime += Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if(jumpTime > buttonTime)
            {
                isJumping = false;
            }
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit
            = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BronzeCoin"))
        {
            score += 1;
            StartCoroutine(DestroyCoin(collision.gameObject));
        }
        else if (collision.gameObject.CompareTag("SilverCoin"))
        {
            score += 2;
            StartCoroutine(DestroyCoin(collision.gameObject));
        }
        else if(collision.gameObject.CompareTag("GoldCoin"))
        {
            score += 3;
            StartCoroutine(DestroyCoin(collision.gameObject));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(isOnEnemy())
            {
                StartCoroutine(KillEnemy(collision.gameObject));
            }
            else
            {
                kill();
            }
        }
        else if(collision.gameObject.CompareTag("Finish"))
        {
            Application.Quit();
        }
    }

    private bool isOnEnemy() 
    {
        //Collider2D collider = collision.collider;
        //Vector2 contactPoint = collision.contacts[0].point;
        //Vector2 center = collider.bounds.center;

        //bool top = contactPoint.y > center.y;
        //return top;
        RaycastHit2D raycastHit
            = Physics2D.BoxCast(circleCollider.bounds.center, circleCollider.bounds.size, 0, Vector2.down, 0.1f, enemyLayer);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private IEnumerator DestroyCoin(GameObject obj)
    {
        //play your sound
        AudioSource coinAudio = obj.GetComponent<AudioSource>();
        if (coinAudio != null)
        {
            coinAudio.time = 0.6f;
            coinAudio.Play();
        }
        obj.GetComponent<BoxCollider2D>().enabled = false;
        obj.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(obj); //this will work after 3 seconds.
    }

    private IEnumerator KillEnemy(GameObject obj)
    {
        //bounce of enemy
        //rb.velocity = new Vector2(rb.velocity.x, 10);
        rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        AudioSource enemyAudio = obj.GetComponent<AudioSource>();
        enemyAudio.Play();
        animator.SetTrigger("Jump");
        score += 4;
        obj.GetComponent<SpriteRenderer>().enabled = false;
        obj.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Destroy(obj);
    }

    private void kill()
    {
        lives--;
        if(lives <= 0)
        {
            SceneManager.LoadScene("Game Over");
            return;
        }
        transform.position = position;
        //Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(currentScene.name);
    }
}
