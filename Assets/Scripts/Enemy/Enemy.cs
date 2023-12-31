using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float idleDuration;
    public float movementSpeed;
    public Transform leftEdge, rightEdge;
    public StateMachine stateMachine;
    public Animator animator {get; private set;}
    public float attackCooldown;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float attackRange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private LayerMask playerLayer;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        stateMachine.StartState(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.UpdateState(this, stateMachine);
    }

    public bool playerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0,Vector2.left,0f,playerLayer);

        if(hit.collider != null) {
            //mendapatkan nyawa dari player
            player = hit.transform.GetComponent<Player>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * attackRange,boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    
    private void DamagePlayer() {
        if (playerInSight()) {
            //damage player
            player.TakeDamage(100);
        }
    }

    public void TakeDamage() {
        stateMachine.SwitchState(this, stateMachine.deathState);
        //GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
    }
}
