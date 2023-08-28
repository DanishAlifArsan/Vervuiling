using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;
    public EnemyIdle idleState = new EnemyIdle();
    public EnemyWalk walkState = new EnemyWalk();
    
    // Start is called before the first frame update
    public void StartState(Enemy enemy)
    {
        currentState = idleState;
        currentState.EnterState(enemy, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchState(Enemy enemy, BaseState state) {
        currentState = state;
        state.EnterState(enemy, this);
    }
}
