using UnityEngine;

public class EnemyIdle : BaseState
{
    private float idleTimer = 0f;
    public override void EnterState(Enemy enemy, StateMachine stateMachine)
    {
        // enemy.animator.SetInteger("AnimState", 0);
    }

    public override void UpdateState(Enemy enemy, StateMachine stateMachine)
    {
        if (enemy.playerInSight())
        {
            idleTimer = 0f;
            stateMachine.SwitchState(enemy, stateMachine.attackState);
        }

        if (idleTimer < enemy.idleDuration)
        {
            idleTimer += Time.deltaTime;
        } else {
            idleTimer = 0f;
            stateMachine.SwitchState(enemy, stateMachine.walkState);
        }
    }
}
