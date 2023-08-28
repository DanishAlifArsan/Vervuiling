using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(Enemy enemy, StateMachine stateMachine);
    public abstract void UpdateState(Enemy enemy, StateMachine stateMachine);
}
