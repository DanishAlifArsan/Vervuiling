using UnityEngine;

public class EnemyAttack : BaseState
{
    private float cooldownTimer;
    public override void EnterState(Enemy enemy, StateMachine stateMachine)
    {
        cooldownTimer = Mathf.Infinity;
    }

    public override void UpdateState(Enemy enemy, StateMachine stateMachine)
    {
        cooldownTimer += Time.deltaTime;

        if(enemy.playerInSight()) {
            if (cooldownTimer >= enemy.attackCooldown) {
                cooldownTimer = 0;
                enemy.animator.SetTrigger("Attack");
            } 
        } else {
            stateMachine.SwitchState(enemy, stateMachine.idleState);
        }
    }
}
