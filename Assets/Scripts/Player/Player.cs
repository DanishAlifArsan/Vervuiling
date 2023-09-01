using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D playerCollider;
    private float horizontalInput;
    private Vector3 respawnPoint;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    // public LayerMask bossLayer;
    [SerializeField] private LayerMask groundLayer;
    private float nextAttackTime = 0f;
    private float coyoteCounter;
    private int jumpCounter;
    public int health = 100;

    [Header("Player Settings")]
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    [SerializeField] private int extraJump;

    void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        //Movement
        horizontalInput = Input.GetAxis("Horizontal");

        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        //     {
        //         Jump();
        //     }
        // if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y >.0)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
        //     }

        // if (onWall())
        // {
        //     rb.gravityScale = 0.5f;
        //     rb.velocity =  new Vector2(horizontalInput * speed, rb.velocity.y);
        // }
        // else
        // {
        //     rb.gravityScale = 1.5f;
        //     rb.velocity =  new Vector2(horizontalInput * speed, rb.velocity.y);

        //     if(isGrounded())
        //     {
        //         coyoteCounter = coyoteTime;
        //         jumpCounter = extraJump;
        //     }
        //     else
        //     {
        //         coyoteCounter -= Time.deltaTime;
        //     }

        // }

        //Animation
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Grounded", isGrounded());

        //Flip Player
        // if(horizontalInput > 0.01f)
        // {
        //     transform.localScale = new Vector2(2, 2);
        // }
        // else if(horizontalInput < -0.01f)
        // {
        //     transform.localScale = new Vector2(-2, 2);
        // }

        

        //Attack
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                anim.SetTrigger("Attack");
            }
        }
    }

    // void Jump()
    // {
    //     if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;
        
    //     if(onWall()){
    //         WallJump();
    //     }
    //     else
    //     {
    //         if(isGrounded())
    //         {
    //             rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    //         }
    //         else
    //         {
    //             if(coyoteCounter > 0)
    //             {
    //                 rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    //             }
    //             else
    //             {
    //                 if (jumpCounter > 0)
    //                 {
    //                     rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    //                     jumpCounter--;
    //                 }
    //             }
    //         }
    //         coyoteCounter = 0;
    //     }
    // }

    // void WallJump()
    // {
    //     rb.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)* wallJumpX, wallJumpY));
    //     wallJumpCooldown = 0;
    // }

    void Attack()
    {
        Collider2D [] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemy)
        {
            if (enemy.tag == "Enemy")
            {
                enemy.GetComponent<Enemy>().TakeDamage();
            }
            else if (enemy.tag == "Boss")
            {
                enemy.GetComponent<BossHealth>().TakeDamage(75);
            }
            
            Debug.Log("Hit " + enemy.name);
        }

        // Collider2D [] hitBoss = Physics2D.OverlapCircleAll(attackPoint, attackRange, bossLayer);

        // foreach(Collider2D boss in hitEnemy)
        // {
        //     boss.GetComponent<BossHealth>().TakeDamage(75);
        // }
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
            Die();
            //teleportSoundEffect.Play();
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.down, 0.5f, groundLayer);
        return raycastHit.collider != null;
    }

    // private bool onWall()
    // {
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.01f, groundLayer);
    //     return raycastHit.collider != null;
    // }

    public void TakeDamage(int damage)
	{
		health -= damage;

		StartCoroutine(DamageAnimation());

		if (health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		transform.position = respawnPoint;
        health = 100;
	}

    IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}
			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}
			yield return new WaitForSeconds(.1f);
		}
	}
}
