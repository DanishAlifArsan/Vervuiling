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

    public Rigidbody2D rb {get; private set;}
    public Animator animator {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        stateMachine = new StateMachine();
        stateMachine.StartState(this);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.UpdateState(this, stateMachine);
    }
}
