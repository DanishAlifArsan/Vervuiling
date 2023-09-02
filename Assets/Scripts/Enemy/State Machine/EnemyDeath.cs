using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeath : BaseState
{
    public override void EnterState(Enemy enemy, StateMachine stateMachine)
    {
        // enemy.animator.SetTrigger("Death");
        enemy.gameObject.SetActive(false);
    }

    public override void UpdateState(Enemy enemy, StateMachine stateMachine)
    {
     
    }
}
