using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private Vector3 respawnPoint;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    private float nextAttackTime = 0f;
    private float coyoteCounter;
    private int jumpCounter;

    [Header("Player Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private int extraJump;

    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip Player
        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Movement
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y >.0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
            }

        if (onWall())
        {
            rb.gravityScale = 0.5f;
            rb.velocity =  new Vector2(horizontalInput * speed, rb.velocity.y);
        }
        else
        {
            rb.gravityScale = 1.5f;
            rb.velocity =  new Vector2(horizontalInput * speed, rb.velocity.y);

            if(isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJump;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }

        }

        //Attack
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;
        
        if(onWall()){
            WallJump();
        }
        else
        {
            if(isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            else
            {
                if(coyoteCounter > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                }
                else
                {
                    if (jumpCounter > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                        jumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;
        }
    }

    void WallJump()
    {
        rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)* wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    void Attack()
    {
        Collider2D [] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<Enemy>().TakeDamage();
            Debug.Log("Hit " + enemy.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Token")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            rb.velocity = new Vector2(0, 0);
            //teleportSoundEffect.Play();
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.5f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.01f, groundLayer);
        return raycastHit.collider != null;
    }
}
